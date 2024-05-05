using FluentValidation;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Models;
using DoctorService.Interfaces;
using FluentValidation.Results;
using DoctorService.Shared;
using DoctorService.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Services;

public class ClinicService(DoctorServiceContext context,
						   IValidator<ClinicDto> dataValidator) : IClinicService, IDisposable
{
	private readonly DoctorServiceContext _context = context;
	private readonly IValidator<ClinicDto> _dataValidator = dataValidator;

	public async Task<Result> Add(ClinicDto model)
	{
		ValidationResult validationResult = _dataValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		try
		{
			Clinic clinic = model.Adapt<Clinic>();
			await _context.Clinics.AddAsync(clinic);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(clinic.Adapt<ClinicDto>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}



	public async Task<Result> Edit(ClinicDto model)
	{
		ValidationResult validationResult = _dataValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		if (model.Id == null)
			return CustomErrors.InvalidData("Id not Assigned!");

		Clinic? oldData = await _context.Clinics.SingleOrDefaultAsync(c => c.Id == model.Id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			Clinic clinic = model.Adapt<Clinic>();
			_context.Clinics.Update(clinic);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessUpdate(clinic.Adapt<ClinicDto>());
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
