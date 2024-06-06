using DoctorService;
using DoctorService.DTOs;
using DoctorService.Entities;
using DoctorService.Enums;
using DoctorService.Models;

namespace MedicalHealthPlus.Interfaces;

public interface IDoctorService
{
	Task<Result> GetInfo(Guid id);
	Task<Result> GetDetail(Guid id);

	Task<Result> GetAllRecommends();
	Task<Result> GetAllInfo(DoctorFilterDto model);
	Task<Result> GetAllDetails(DoctorFilterDto model);

	Task<Result> Add(AddDoctorDto model);
	Task<Result> Edit(EditDoctorDto model);
	Task<Result> ChangeStatus(ChangeDoctorStatusDto model);
	Task<Result> ChangeLineStatus(ChangeDoctorLineStatusDto model);
}
