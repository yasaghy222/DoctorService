using System.Linq.Expressions;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Entities;
using DoctorService.Enums;
using DoctorService.Models;
using DoctorService.Shared;
using FileService;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using MedicalHealthPlus.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoctorService;

public class DoctorService(DoctorServiceContext context,
											  ILogger<FileService.FileService> fileLogger,
											  IValidator<AddDoctorDto> addValidator,
											  IValidator<EditDoctorDto> editValidator,
											  IValidator<AddFileDto> fileValidator) : IDoctorService, IDisposable
{
	private readonly DoctorServiceContext _context = context;
	private readonly FileService.FileService _fileService = new(fileValidator, fileLogger);
	private readonly IValidator<AddDoctorDto> _addValidator = addValidator;
	private readonly IValidator<EditDoctorDto> _editValidator = editValidator;

	private async Task<Result> Get(Guid id, Expression<Func<Doctor, bool>> predicate)
	{
		if (id == Guid.Empty)
			return CustomErrors.InvalidData("Id not Assigned!");

		Doctor? doctor = await _context.Doctors.SingleOrDefaultAsync(predicate);
		if (doctor == null)
			return CustomErrors.NotFoundData();

		return CustomResults.SuccessOperation(doctor);
	}
	public async Task<Result> GetInfo(Guid id)
	{
		Result result = await Get(id, d => d.Id == id && d.Status == DoctorStatus.Confirmed);

		if (result.Status)
			return CustomResults.SuccessOperation(result.Data.Adapt<DoctorInfo>());
		else
			return result;
	}
	public async Task<Result> GetDetail(Guid id)
	{
		Result result = await Get(id, d => d.Id == id);

		if (result.Status)
			return CustomResults.SuccessOperation(result.Data.Adapt<DoctorDetail>());
		else
			return result;
	}

	private static DateTime? GetNearestVisitDate(ICollection<VisitPlan>? visitPlans)
	{
		if (visitPlans == null || visitPlans.Count == 0)
			return null;

		DateTime? dateTime = null;

		IEnumerable<DateTime> dates = from vp in visitPlans
									  where vp.Date.CompareTo(DateOnly.FromDateTime(DateTime.Now)) <= 0 &&
											vp.StartTime.CompareTo(TimeOnly.FromDateTime(DateTime.Now)) <= 0
									  select new DateTime(vp.Date, vp.StartTime);
		dateTime = dates.FirstOrDefault();

		return dateTime;
	}
	public async Task<Result> GetAllRecommends()
	{
		List<RecommendedDoctor> doctors = await _context.Doctors.IgnoreAutoIncludes()
																												.Include(d => d.Specialty)
																												.Where(d => d.Status == DoctorStatus.Confirmed)
																												.Select(d => new RecommendedDoctor
																												{
																													ImagePath = d.ImagePath,
																													Id = d.Id,
																													FullName = $"{d.Name} {d.Family}",
																													SpecialtyTitle = d.Specialty.Title,
																													Rate = d.Rate,
																													LineStatus = d.LineStatus
																												})
																												.OrderByDescending(d => d.Rate)
																												.Take(6)
																												.ToListAsync();

		return CustomResults.SuccessOperation(doctors);
	}
	public async Task<Result> GetAllInfo(DoctorFilterDto model)
	{
		IQueryable<DoctorInfo> query = from doctor in _context.Doctors
										.Where(d => d.Status == DoctorStatus.Confirmed)
										.Skip((model.PageIndex - 1) * model.PageSize)
										.Take(model.PageSize)
									   select new DoctorInfo
									   {
										   ImagePath = doctor.ImagePath,
										   Id = doctor.Id,
										   Name = doctor.Name,
										   Family = doctor.Family,
										   FullName = $"{doctor.Name} {doctor.Family}",
										   MedicalSysCode = doctor.MedicalSysCode,
										   Content = doctor.Content,
										   SpecialtyId = doctor.SpecialtyId,
										   SpecialtyTitle = doctor.Specialty.Title,
										   Rate = doctor.Rate,
										   RaterCount = doctor.RaterCount,
										   SuccessConsolationCount = doctor.SuccessConsolationCount,
										   SuccessReservationCount = doctor.SuccessReservationCount,
										   HasTelCounseling = doctor.HasTelCounseling,
										   HasTextCounseling = doctor.HasTextCounseling,
										   OnlinePlans = doctor.OnlinePlans != null ? doctor.OnlinePlans.Adapt<ICollection<OnlinePlanDto>>() : null,
										   AcceptVisit = doctor.AcceptVisit,
										   ClinicId = doctor.ClinicId,
										   Clinic = doctor.Clinic != null ? doctor.Clinic.Adapt<ClinicDto>() : null,
										   VisitPlans = doctor.VisitPlans != null ? doctor.VisitPlans.Adapt<ICollection<VisitPlanDto>>() : null,
										   NearestVisitDate = GetNearestVisitDate(doctor.VisitPlans),
										   LineStatus = doctor.LineStatus
									   };
		query = model.ServiceType switch
		{
			DoctorServiceType.HasTelCounseling => query.Where(d => d.HasTelCounseling),
			DoctorServiceType.HasTextCounseling => query.Where(d => d.HasTextCounseling),
			DoctorServiceType.HasReservation => query.Where(d => d.AcceptVisit),
			DoctorServiceType.All => query,
			_ => query
		};

		query = model.Order switch
		{
			DoctorFilterOrder.NearestReservationTime => query.OrderByDescending(d => d.NearestVisitDate),
			DoctorFilterOrder.Rate => query.OrderByDescending(d => d.Rate),
			DoctorFilterOrder.SuccessReservationCount => query.OrderByDescending(d => d.SuccessReservationCount),
			DoctorFilterOrder.Default => query.OrderBy(d => d.Name),
			_ => query.OrderBy(d => d.Name)
		};

		if (model.LocationId != null)
			query = query.Where(d => d.Clinic != null &&
								d.Clinic.LocationId == model.LocationId);

		return CustomResults.SuccessOperation(await query.ToListAsync());
	}
	public async Task<Result> GetAllDetails(DoctorFilterDto model)
	{
		IQueryable<DoctorDetail> query = from doctor in _context.Doctors
										.Skip((model.PageIndex - 1) * model.PageSize)
										.Take(model.PageSize)
										 select new DoctorDetail
										 {
											 ImagePath = doctor.ImagePath,
											 Name = doctor.Name,
											 Family = doctor.Family,
											 FullName = $"{doctor.Name} {doctor.Family}",
											 MedicalSysCode = doctor.MedicalSysCode,
											 Content = doctor.Content,
											 SpecialtyId = doctor.SpecialtyId,
											 SpecialtyTitle = doctor.Specialty.Title,
											 Rate = doctor.Rate,
											 RaterCount = doctor.RaterCount,
											 SuccessConsolationCount = doctor.SuccessConsolationCount,
											 SuccessReservationCount = doctor.SuccessReservationCount,
											 HasTelCounseling = doctor.HasTelCounseling,
											 HasTextCounseling = doctor.HasTextCounseling,
											 OnlinePlans = doctor.OnlinePlans != null ? doctor.OnlinePlans.Adapt<ICollection<OnlinePlanDto>>() : null,
											 AcceptVisit = doctor.AcceptVisit,
											 ClinicId = doctor.ClinicId,
											 Clinic = doctor.Clinic != null ? doctor.Clinic.Adapt<ClinicDto>() : null,
											 VisitPlans = doctor.VisitPlans != null ? doctor.VisitPlans.Adapt<ICollection<VisitPlanDto>>() : null,
											 NearestVisitDate = GetNearestVisitDate(doctor.VisitPlans),
											 LineStatus = doctor.LineStatus,
											 Status = doctor.Status,
											 CreateAt = doctor.CreateAt,
											 ModifyAt = doctor.ModifyAt,
										 };
		query = model.ServiceType switch
		{
			DoctorServiceType.HasTelCounseling => query.Where(d => d.HasTelCounseling),
			DoctorServiceType.HasTextCounseling => query.Where(d => d.HasTextCounseling),
			DoctorServiceType.HasReservation => query.Where(d => d.AcceptVisit),
			DoctorServiceType.All => query,
			_ => query
		};

		query = model.Order switch
		{
			DoctorFilterOrder.NearestReservationTime => query.OrderByDescending(d => d.NearestVisitDate),
			DoctorFilterOrder.Rate => query.OrderByDescending(d => d.Rate),
			DoctorFilterOrder.SuccessReservationCount => query.OrderByDescending(d => d.SuccessReservationCount),
			DoctorFilterOrder.Default => query.OrderBy(d => d.Name),
			_ => query.OrderBy(d => d.Name)
		};

		if (model.LocationId != null)
			query = query.Where(d => d.Clinic != null &&
								d.Clinic.LocationId == model.LocationId);

		return CustomResults.SuccessOperation(await query.ToListAsync());
	}

	public async Task<Result> Add(AddDoctorDto model)
	{
		ValidationResult validationResult = _addValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		Specialty? specialty = await _context.Specialties.SingleOrDefaultAsync(s => s.Id == model.SpecialtyId);
		if (specialty == null)
			return CustomErrors.NotFoundData("تخصص انتخاب شده یافت نشد!");


		Doctor doctor = model.Adapt<Doctor>();

		Result fileResult = await _fileService.Add(new(doctor.Id, model.Image, "Doctor"));
		if (!fileResult.Status)
			return fileResult;

		doctor.ImagePath = fileResult.Data?.ToString() ?? "";

		try
		{
			await _context.Doctors.AddAsync(doctor);
			await _context.SaveChangesAsync();

			doctor.Specialty = specialty;
			return CustomResults.SuccessCreation(doctor.Adapt<DoctorDetail>());
		}
		catch (Exception e)
		{
			_fileService.Delete(doctor.ImagePath);
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> Edit(EditDoctorDto model)
	{
		ValidationResult validationResult = _editValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		Doctor? oldData = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == model.Id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		string oldPath = oldData.ImagePath;

		_context.Entry(oldData).State = EntityState.Detached;
		oldData = model.Adapt<Doctor>();
		oldData.ImagePath = oldPath;

		if (model.Image != null)
		{
			Result fileResult = await _fileService.Add(new(oldData.Id, model.Image, "Doctor"));
			if (!fileResult.Status)
				return fileResult;

			oldData.ImagePath = fileResult.Data?.ToString() ?? "";
		}

		try
		{
			_context.Doctors.Update(oldData);
			await _context.SaveChangesAsync();

			if (model.Image != null)
			{
				Result fileResult = _fileService.Delete(oldPath);
				if (!fileResult.Status)
					return fileResult;
			}

			return CustomResults.SuccessUpdate(oldData.Adapt<DoctorInfo>());
		}
		catch (Exception e)
		{
			if (model.Image != null)
				_fileService.Delete(oldData.ImagePath);

			return CustomErrors.InternalServer(e.Message);
		}
	}
	public async Task<Result> ChangeStatus(Guid id, DoctorStatus status, string? statusDescription)
	{
		Doctor? oldData = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			int effectedRowCount = await _context.Doctors.Where(d => d.Id == id)
														  .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.Status, status)
																															  .SetProperty(d => d.StatusDescription, statusDescription));

			if (effectedRowCount == 1)
				return CustomResults.SuccessUpdate(oldData.Adapt<DoctorInfo>());
			else
				return CustomErrors.NotFoundData();
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}
	public async Task<Result> ChangeLineStatus(Guid id, DoctorLineStatus lineStatus)
	{
		Doctor? oldData = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			int effectedRowCount = await _context.Doctors.Where(d => d.Id == id)
														  .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.LineStatus, lineStatus));

			if (effectedRowCount == 1)
				return CustomResults.SuccessUpdate(oldData.Adapt<DoctorInfo>());
			else
				return CustomErrors.NotFoundData();
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}
