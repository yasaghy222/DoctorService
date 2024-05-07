using DoctorService.DTOs;
using DoctorService.Models;

namespace DoctorService.Interfaces;

public interface ILocationService
{
	Task<Result> GetAll(Guid? parentId);

	Task<Result> Add(LocationDto model);
	Task<Result> Edit(LocationDto model);
}
