using DoctorService.DTOs;
using FluentValidation;

namespace DoctorService.Validations;

public class ClinicValidations : AbstractValidator<ClinicDto>
{
	public ClinicValidations()
	{
		RuleFor(c => c.LocationId).NotNull()
								  .NotEmpty();

		RuleFor(c => c.Address).NotEmpty()
							   .MaximumLength(200);

		RuleFor(c => c.Content).MaximumLength(500);
	}
}
