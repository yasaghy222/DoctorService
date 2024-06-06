using FluentValidation;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Models;
using Microsoft.AspNetCore.Mvc;
using DoctorService.Services;
using FileService;

namespace DoctorService.Controllers;

[ApiController]
[Route("[controller]")]
public class SpecialtyController(DoctorServiceContext context,
							ILogger<SpecialtyService> logger,
							  IValidator<AddSpecialtyDto> addValidator,
							  IValidator<EditSpecialtyDto> editValidator,
							  IValidator<AddFileDto> fileValidator) : ControllerBase
{
	private readonly SpecialtyService _service = new(context, logger, addValidator, editValidator, fileValidator);

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		Result result = await _service.GetAll();
		return StatusCode(result.StatusCode, result);
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromForm] EditSpecialtyDto model)
	{
		Result result = await _service.Edit(model);
		return StatusCode(result.StatusCode, result);
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromForm] AddSpecialtyDto model)
	{
		Result result = await _service.Add(model);
		return StatusCode(result.StatusCode, result);
	}
}
