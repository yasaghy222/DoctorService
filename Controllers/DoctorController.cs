using FluentValidation;
using DoctorService.Data;
using DoctorService.DTOs;
using DoctorService.Models;
using Microsoft.AspNetCore.Mvc;
using DoctorService.Enums;
using FileService;

namespace DoctorService.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorController(
					DoctorServiceContext context,
					ILogger<DoctorController> logger,
					IValidator<AddDoctorDto> addValidator,
					IValidator<EditDoctorDto> editValidator,
					IValidator<AddFileDto> fileValidator) : ControllerBase
{

	private ILogger<DoctorController> _logger { get; init; } = logger;
	private readonly DoctorService _service = new(context, addValidator, editValidator, fileValidator);

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
	public async Task<IActionResult> Put([FromForm] EditDoctorDto model)
	{
		Result result = await _service.Edit(model);
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromForm] AddDoctorDto model)
	{
		Result result = await _service.Add(model);
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPatch]
	[Route("/[controller]/Status")]
	public async Task<IActionResult> Patch(ChangeDoctorStatusDto model)
	{
		Result result = await _service.ChangeStatus(model);
		return StatusCode(result.StatusCode, result.Data);
	}

	[HttpPatch]
	[Route("/[controller]/LineStatus")]
	public async Task<IActionResult> Patch(ChangeDoctorLineStatusDto model)
	{
		Result result = await _service.ChangeLineStatus(model);
		return StatusCode(result.StatusCode, result.Data);
	}
}
