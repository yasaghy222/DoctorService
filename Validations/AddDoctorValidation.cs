﻿using DoctorService.DTOs;
using FluentValidation;

namespace DoctorService.Validations;

public class AddDoctorValidation : AbstractValidator<AddDoctorDto>
{
	public AddDoctorValidation()
	{
		RuleFor(d => d.Name).NotEmpty()
							.NotNull()
							.MaximumLength(100);

		RuleFor(d => d.Family).NotEmpty()
							  .NotNull()
							  .MaximumLength(100);

		RuleFor(d => d.Phone).NotEmpty()
							 .NotNull()
							 .Matches(@"/((0?9)|(\+?989))\d{2}\W?\d{3}\W?\d{4}/g")
							 .WithMessage("شماره وارد شده معتبر نمی باشد!");


		RuleFor(d => d.Email).EmailAddress()
							 .WithMessage("آدرس ایمیل وارد شده معتبر نمی باشد!");

		RuleFor(d => d.MedicalSysCode).NotEmpty()
									  .NotNull();

		RuleFor(d => d.SpecialtyId).NotEmpty()
								   .NotNull();
	}
}
