using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Entities;
using DoctorService.Interfaces;
using DoctorService.Models;
using DoctorService.Shared;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Services;

public class VisitPlanService(DoctorServiceContext context,
							  IValidator<VisitPlanDto> dataValidator,
							  IValidator<VisitPlanFilterDto> filterValidator) : IVisitPlanService, IDisposable
{
	private readonly DoctorServiceContext _context = context;
	private readonly IValidator<VisitPlanDto> _dataValidator = dataValidator;
	private readonly IValidator<VisitPlanFilterDto> _filterValidator = filterValidator;

	public async Task<Result> GetAll(VisitPlanFilterDto model)
	{
		ValidationResult validationResult = _filterValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		bool isDoctorExist = await _context.Doctors.AnyAsync(d => d.Id == model.DoctorId);
		if (isDoctorExist)
			return CustomErrors.NotFoundData();

		List<VisitPlan> visitPlans = _context.VisitPlans.Where(vp => vp.DoctorId == model.DoctorId &&
																	 vp.Date >= model.FromDate &&
																	 vp.Date <= model.ToDate)
														.ToList();

		return CustomResults.SuccessOperation(visitPlans.Adapt<List<VisitPlanDto>>());
	}

	public async Task<Result> Add(VisitPlanDto model)
	{
		ValidationResult validationResult = _dataValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		try
		{
			VisitPlan visitPlan = model.Adapt<VisitPlan>();
			await _context.VisitPlans.AddAsync(visitPlan);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(visitPlan.Adapt<VisitPlanDto>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> Edit(VisitPlanDto model)
	{
		ValidationResult validationResult = _dataValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		if (model.Id == null)
			return CustomErrors.InvalidData("Id not Assigned!");

		VisitPlan? oldData = await _context.VisitPlans.SingleOrDefaultAsync(l => l.Id == model.Id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			VisitPlan visitPlan = model.Adapt<VisitPlan>();
			_context.VisitPlans.Update(visitPlan);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessUpdate(visitPlan.Adapt<VisitPlanDto>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> Delete(Guid Id)
	{
		VisitPlan? visitPlan = await _context.VisitPlans.SingleOrDefaultAsync(vp => vp.Id == Id);
		if (visitPlan == null)
			return CustomErrors.NotFoundData();

		try
		{
			_context.VisitPlans.Remove(visitPlan);
			await _context.SaveChangesAsync();

			return CustomResults.SuccessOperation();
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
