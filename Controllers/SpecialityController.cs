﻿using FluentValidation;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Models;
using Microsoft.AspNetCore.Mvc;
using DoctorService.Enums;
using DoctorService.Services;

namespace DoctorService.Controllers;

[ApiController]
[Route("[controller]")]
public class SpecialtyController(DoctorServiceContext context,
							  IValidator<AddSpecialtyDto> addValidator,
							  IValidator<EditSpecialtyDto> editValidator) : ControllerBase
{
	private readonly SpecialtyService _service = new(context, addValidator, editValidator);

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		Result result = await _service.GetAll();
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromForm] AddSpecialtyDto model)
	{
		Result result = await _service.Add(model);
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromForm] EditSpecialtyDto model)
	{
		Result result = await _service.Edit(model);
		return StatusCode(result.StatusCode, result.Data);
	}
}
