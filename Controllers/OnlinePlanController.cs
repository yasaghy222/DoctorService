using FluentValidation;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Models;
using Microsoft.AspNetCore.Mvc;
using DoctorService.Services;

namespace DoctorService.Controllers;

[ApiController]
[Route("[controller]")]
public class OnlinePlanController(DoctorServiceContext context,
							  IValidator<OnlinePlanDto> dataValidator) : ControllerBase
{
	private readonly OnlinePlanService _service = new(context, dataValidator);

	[HttpGet]
	[Route("/[controller]/{doctorId}")]
	public async Task<IActionResult> Get(Guid doctorId)
	{
		Result result = await _service.GetAll(doctorId);
		return StatusCode(result.StatusCode, result);
	}

	[HttpPut]
	public async Task<IActionResult> Put(OnlinePlanDto model)
	{
		Result result = await _service.Edit(model);
		return StatusCode(result.StatusCode, result);
	}

	[HttpPost]
	public async Task<IActionResult> Post(OnlinePlanDto model)
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
