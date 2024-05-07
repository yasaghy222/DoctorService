using DoctorService.DTOs;
using DoctorService.Models;

namespace DoctorService.Interfaces;

public interface IClinicService
{
	Task<Result> Add(ClinicDto model);
	Task<Result> Edit(ClinicDto model);
}
