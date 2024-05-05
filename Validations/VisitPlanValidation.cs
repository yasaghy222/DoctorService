using DoctorService.DTOs;
using FluentValidation;

namespace DoctorService.Validations;

public class VisitPlanValidation : AbstractValidator<VisitPlanDto>
{
	public VisitPlanValidation()
	{
		RuleFor(vp => vp.Date).NotNull()
							  .NotEmpty()
							  .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
							  .WithMessage("تاریخ انتخابی باید بزرگتر از تاریخ امروز باشد!");

		RuleFor(vp => vp.StartTime).NotNull()
								   .NotEmpty();

		RuleFor(vp => vp.EndTime).NotNull()
								 .NotEmpty()
								 .GreaterThan(vp => vp.StartTime)
								 .WithMessage("ساعت پایانی نمی تواند کوچکتر از ساعت شروع باشد!");
	}
}
