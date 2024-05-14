using DoctorService.DTOs;
using FluentValidation;

namespace DoctorService.Validations;

public class EditDoctorValidation : AbstractValidator<EditDoctorDto>
{
	public EditDoctorValidation()
	{
		RuleFor(d => d.Image).SetValidator(d => new FileValidator<EditDoctorDto>(d.Image));

		RuleFor(d => d.Id).NotEmpty()
						  .NotNull();

		RuleFor(d => d.Name).NotEmpty()
							.NotNull()
							.MaximumLength(100);

		RuleFor(d => d.Family).NotEmpty()
							  .NotNull()
							  .MaximumLength(100);

		RuleFor(d => d.MedicalSysCode).NotEmpty()
									  .NotNull();

		RuleFor(d => d.SpecialtyId).NotEmpty()
								   .NotNull();
	}
}
