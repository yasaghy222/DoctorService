using FluentValidation;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Models;
using Microsoft.AspNetCore.Mvc;
using DoctorService.Services;

namespace DoctorService.Controllers;

[ApiController]
[Route("[controller]")]
public class VisitPlanController(DoctorServiceContext context,
							  IValidator<VisitPlanDto> dataValidator,
							  IValidator<VisitPlanFilterDto> filterValidator) : ControllerBase
{
	private readonly VisitPlanService _service = new(context, dataValidator, filterValidator);

	[HttpGet]
	[Route("/[controller]/{doctorId}")]
	public async Task<IActionResult> Get(Guid doctorId, [FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate)
	{
		VisitPlanFilterDto filterDto = new(doctorId, fromDate, toDate);
		Result result = await _service.GetAll(filterDto);

		return StatusCode(result.StatusCode, result);
	}

	[HttpPut]
	public async Task<IActionResult> Put(VisitPlanDto model)
	{
		Result result = await _service.Edit(model);
		return StatusCode(result.StatusCode, result);
	}

	[HttpPost]
	public async Task<IActionResult> Post(VisitPlanDto model)
	{
		Result result = await _service.Add(model);
		return StatusCode(result.StatusCode, result);
	}

	[HttpDelete]
	[Route("/[controller]/{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		Result result = await _service.Delete(id);
		return StatusCode(result.StatusCode, result);
	}
}
