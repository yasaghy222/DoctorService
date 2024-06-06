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
public class LocationController(DoctorServiceContext context,
							  IValidator<LocationDto> dataValidator) : ControllerBase
{
	private readonly LocationService _service = new(context, dataValidator);

	[HttpGet]
	public async Task<IActionResult> Get(Guid? parentId)
	{
		Result result = await _service.GetAll(parentId);
		return StatusCode(result.StatusCode, result);
	}

	[HttpPut]
	public async Task<IActionResult> Put(LocationDto model)
	{
		Result result = await _service.Edit(model);
		return StatusCode(result.StatusCode, result);
	}

	[HttpPost]
	public async Task<IActionResult> Post(LocationDto model)
	{
		Result result = await _service.Add(model);
		return StatusCode(result.StatusCode, result);
	}
}
