using DoctorService.DTOs;
using FluentValidation;

namespace DoctorService.Validations;

public class AddSpecialtyValidation : AbstractValidator<AddSpecialtyDto>
{
	public AddSpecialtyValidation()
	{
		RuleFor(s => s.Image).SetValidator(s => new FileValidator<AddSpecialtyDto>(s.Image));

		RuleFor(s => s.Title).NotNull()
							 .NotEmpty();

		RuleFor(s => s.Content).MaximumLength(500);
	}
}
