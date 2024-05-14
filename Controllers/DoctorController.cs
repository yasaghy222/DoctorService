using FluentValidation;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Models;
using Microsoft.AspNetCore.Mvc;
using DoctorService.Enums;

namespace DoctorService.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorController(DoctorServiceContext context,
							  IValidator<AddDoctorDto> addValidator,
							  IValidator<EditDoctorDto> editValidator) : ControllerBase
{
	private readonly DoctorService _service = new(context, addValidator, editValidator);

	[HttpGet]
	[Route("/[controller]/{type}/{id}")]
	public async Task<IActionResult> Get(GetDoctorType type, Guid id)
	{
		Result result = type switch
		{
			GetDoctorType.Info => await _service.GetInfo(id),
			GetDoctorType.Detail => await _service.GetDetail(id),
			_ => await _service.GetInfo(id)
		};
		return StatusCode(result.StatusCode, result.Data);
	}
	[HttpGet]
	[Route("/[controller]/{type}")]
	public async Task<IActionResult> Get(GetDoctorType type,
																	[FromQuery] int pageIndex,
																	[FromQuery] int pageSize,
																	[FromQuery] DoctorServiceType serviceType,
																	[FromQuery] DoctorFilterOrder order,
																	[FromQuery] Guid? locationId)
	{
		DoctorFilterDto filterDto = new(pageIndex, pageSize, serviceType, order, locationId);

		Result result = type switch
		{
			GetDoctorType.Info => await _service.GetAllInfo(filterDto),
			GetDoctorType.Detail => await _service.GetAllDetails(filterDto),
			GetDoctorType.Recommended => await _service.GetAllRecommends(),
			_ => await _service.GetAllInfo(filterDto)
		};
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromForm] AddDoctorDto model)
	{
		Result result = await _service.Add(model);
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromForm] EditDoctorDto model)
	{
		Result result = await _service.Edit(model);
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPatch]
	[Route("/[controller]/Status/{id}")]
	public async Task<IActionResult> Patch(Guid id, DoctorStatus status, string? statusDescription)
	{
		Result result = await _service.ChangeStatus(id, status, statusDescription);
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPatch]
	[Route("/[controller]/lineStatus/{id}/{status}")]
	public async Task<IActionResult> Patch(Guid id, DoctorLineStatus status)
	{
		Result result = await _service.ChangeLineStatus(id, status);
		return StatusCode(result.StatusCode, result.Data);
	}
}
