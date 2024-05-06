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

public class OnlinePlanService(DoctorServiceContext context,
							   IValidator<OnlinePlanDto> dataValidator) : IOnlinePlanService, IDisposable
{
	private readonly DoctorServiceContext _context = context;
	private readonly IValidator<OnlinePlanDto> _dataValidator = dataValidator;

	public async Task<Result> GetAll(Guid doctorId)
	{
		bool isDoctorExist = await _context.Doctors.AnyAsync(d => d.Id == doctorId);
		if (isDoctorExist)
			return CustomErrors.NotFoundData();

		List<OnlinePlan> onlinePlans = await _context.OnlinePlans.Where(vp => vp.DoctorId == doctorId).ToListAsync();

		return CustomResults.SuccessOperation(onlinePlans.Adapt<List<OnlinePlanDto>>());
	}

	public async Task<Result> Add(OnlinePlanDto model)
	{
		ValidationResult validationResult = _dataValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		try
		{
			OnlinePlan onlinePlan = model.Adapt<OnlinePlan>();
			await _context.OnlinePlans.AddAsync(onlinePlan);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(onlinePlan.Adapt<OnlinePlanDto>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> Edit(OnlinePlanDto model)
	{
		ValidationResult validationResult = _dataValidator.Validate(model);
		if (validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		if (model.Id == null)
			return CustomErrors.InvalidData("Id not Assigned!");

		OnlinePlan? oldData = await _context.OnlinePlans.SingleOrDefaultAsync(l => l.Id == model.Id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			_context.Entry(oldData).State = EntityState.Detached;

			OnlinePlan onlinePlan = model.Adapt<OnlinePlan>();
			_context.OnlinePlans.Update(onlinePlan);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessUpdate(onlinePlan.Adapt<OnlinePlan>());
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

			return CustomResults.SuccessDelete();
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
