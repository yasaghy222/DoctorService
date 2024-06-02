using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Entities;
using DoctorService.Interfaces;
using DoctorService.Models;
using DoctorService.Shared;
using FileService;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Services;

public class SpecialtyService(DoctorServiceContext context,
							  ILogger<SpecialtyService> logger,
							  ILogger<FileService.FileService> fileLogger,
							  IValidator<AddSpecialtyDto> addValidator,
							  IValidator<EditSpecialtyDto> editValidator,
							  IValidator<AddFileDto> fileValidator) : ISpecialtyService, IDisposable
{

	private ILogger<SpecialtyService> _logger { get; init; } = logger;
	private readonly DoctorServiceContext _context = context;
	private readonly FileService.FileService _fileService = new(fileValidator, fileLogger);

	private readonly IValidator<AddSpecialtyDto> _addValidator = addValidator;
	private readonly IValidator<EditSpecialtyDto> _editValidator = editValidator;


	public async Task<Result> GetAll()
	{
		List<Specialty> Specialties = await _context.Specialties.ToListAsync();
		return CustomResults.SuccessOperation(Specialties.Adapt<List<SpecialtyInfo>>());
	}


	public async Task<Result> Add(AddSpecialtyDto model)
	{
		ValidationResult validationResult = _addValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		Specialty specialty = model.Adapt<Specialty>();

		Result fileResult = await _fileService.Add(new(specialty.Id, model.Image, "Specialty"));
		if (!fileResult.Status)
			return fileResult;

		specialty.ImagePath = fileResult.Data?.ToString() ?? "";

		try
		{
			await _context.Specialties.AddAsync(specialty);
			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(specialty.Adapt<SpecialtyInfo>());
		}
		catch (Exception e)
		{
			_logger.LogDebug(e.Message, e);
			_fileService.Delete(specialty.ImagePath);
			return CustomErrors.InternalServer();
		}
	}

	public async Task<Result> Edit(EditSpecialtyDto model)
	{
		ValidationResult validationResult = _editValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		Specialty? oldData = await _context.Specialties.SingleOrDefaultAsync(l => l.Id == model.Id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		string oldPath = oldData.ImagePath;

		_context.Entry(oldData).State = EntityState.Detached;
		oldData = model.Adapt<Specialty>();
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
			_context.Specialties.Update(oldData);
			await _context.SaveChangesAsync();

			if (model.Image != null)
			{
				Result fileResult = _fileService.Delete(oldPath);
				if (!fileResult.Status)
					return fileResult;
			}

			return CustomResults.SuccessUpdate(oldData.Adapt<SpecialtyInfo>());
		}
		catch (Exception e)
		{
			if (model.Image != null)
				_fileService.Delete(oldData.ImagePath);

			_logger.LogDebug(e.Message, e);
			return CustomErrors.InternalServer();
		}
	}


	public void Dispose()
	{
		_context.Dispose();
	}
}
