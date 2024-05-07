using DoctorService.DTOs;
using DoctorService.Models;

namespace DoctorService.Interfaces;

public interface IOnlinePlanService
{
	Task<Result> GetAll(Guid doctorId);
	Task<Result> Add(OnlinePlanDto model);
	Task<Result> Edit(OnlinePlanDto model);
	Task<Result> Delete(Guid Id);
}
