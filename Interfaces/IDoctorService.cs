using DoctorService;
using DoctorService.DTOs;
using DoctorService.Entities;
using DoctorService.Enums;
using DoctorService.Models;

namespace MedicalHealthPlus.Interfaces;

public interface IDoctorService
{
	Task<Result> GetInfo(Guid id);
	Task<Result> GetAllInfo(DoctorFilterDto model);
	Task<Result> GetRecommendedDoctors();

	Task<Result> Add(AddDoctorDto model);
	Task<Result> Edit(EditDoctorDto model);
	Task<Result> ChangeStatus(Guid id, DoctorStatus status);
}
