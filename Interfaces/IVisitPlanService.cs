using DoctorService.DTOs;
using DoctorService.Models;

namespace DoctorService.Interfaces;

public interface IVisitPlanService
{
	Task<Result> GetAll(VisitPlanFilterDto model);
	Task<Result> Add(VisitPlanDto model);
	Task<Result> Edit(VisitPlanDto model);
	Task<Result> Delete(Guid Id);
}
