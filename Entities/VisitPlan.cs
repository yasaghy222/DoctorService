using DoctorService.Enums;

namespace DoctorService.Entities;
public class VisitPlan : BaseEntity
{
	public DateOnly Date { get; set; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }

	public Guid DoctorId { get; set; }
	public required Doctor Doctor { get; set; }

	public VisitPlanStatus Status { get; set; } = VisitPlanStatus.Active;
}