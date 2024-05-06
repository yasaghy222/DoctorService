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

public class SpecialtyService(DoctorServiceContext context,
							  IValidator<SpecialtyDto> dataValidator) : ISpecialtyService, IDisposable
{

	private readonly DoctorServiceContext _context = context;
	private readonly IValidator<SpecialtyDto> _dataValidator = dataValidator;


	public async Task<Result> GetAll()
	{
		List<Specialty> Specialties = await _context.Specialties.ToListAsync();
		return CustomResults.SuccessOperation(Specialties.Adapt<List<SpecialtyInfo>>());
	}


	public async Task<Result> Add(SpecialtyDto model)
	{
		ValidationResult validationResult = _dataValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		try
		{
			Specialty specialty = model.Adapt<Specialty>();
			await _context.Specialties.AddAsync(specialty);

			await _context.SaveChangesAsync();

			//TODO: Add Image With FileStorageModule

			return CustomResults.SuccessCreation(specialty.Adapt<SpecialtyDto>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> Edit(SpecialtyDto model)
	{
		ValidationResult validationResult = _dataValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		if (model.Id == null)
			return CustomErrors.InvalidData("Id not Assigned!");

		Specialty? oldData = await _context.Specialties.SingleOrDefaultAsync(l => l.Id == model.Id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			_context.Entry(oldData).State = EntityState.Detached;

			Specialty specialty = model.Adapt<Specialty>();
			_context.Specialties.Update(specialty);

			await _context.SaveChangesAsync();

			//TODO: Update Image With FileStorageModule

			return CustomResults.SuccessUpdate(specialty.Adapt<LocationDto>());
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
