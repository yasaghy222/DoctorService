using DoctorService.DTOs;
using FluentValidation;

namespace DoctorService.Validations;

public class VisitPlanFilterValidation : AbstractValidator<VisitPlanFilterDto>
{
	public VisitPlanFilterValidation()
	{
		RuleFor(vpf => vpf.DoctorId).NotEmpty()
									.NotNull();

		RuleFor(vpf => vpf.FromDate).NotEmpty()
									.NotNull()
									.GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
									.WithMessage("تاریخ شروع نمی تواند کمتر از تاریخ امروز باشد!");

		RuleFor(vpf => vpf.FromDate).NotEmpty()
									.NotNull()
									.GreaterThan(vpf => vpf.FromDate)
									.WithMessage("تاریخ پایان نمی تواند کمتر از تاریخ شروع باشد!");
	}
}
