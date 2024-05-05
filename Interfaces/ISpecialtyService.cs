using DoctorService.DTOs;
using DoctorService.Models;

namespace DoctorService.Interfaces;

public interface ISpecialtyService
{
	Task<Result> GetAll();
	Task<Result> Add(SpecialtyDto model);
	Task<Result> Edit(SpecialtyDto model);
}
