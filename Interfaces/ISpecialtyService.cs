﻿using DoctorService.DTOs;
using DoctorService.Models;

namespace DoctorService.Interfaces;

public interface ISpecialtyService
{
	Task<Result> GetAll();
	Task<Result> Add(AddSpecialtyDto model);
	Task<Result> Edit(EditSpecialtyDto model);
}
