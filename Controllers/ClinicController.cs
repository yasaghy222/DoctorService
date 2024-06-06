using FluentValidation;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Models;
using Microsoft.AspNetCore.Mvc;
using DoctorService.Enums;
using DoctorService.Services;

namespace DoctorService.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicController(DoctorServiceContext context,
							  IValidator<ClinicDto> dataValidator) : ControllerBase
{
	private readonly ClinicService _service = new(context, dataValidator);

	[HttpPut]
	public async Task<IActionResult> Put(ClinicDto model)
	{
		Result result = await _service.Edit(model);
		return StatusCode(result.StatusCode, result);
	}

	[HttpPost]
	public async Task<IActionResult> Post(ClinicDto model)
	{
		Result result = await _service.Add(model);
		return StatusCode(result.StatusCode, result);
	}
}
