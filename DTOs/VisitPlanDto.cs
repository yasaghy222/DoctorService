using DoctorService.Enums;

namespace DoctorService.DTOs;

public class VisitPlanDto
{
	public Guid? Id { get; set; }
	public required DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	public required TimeOnly StartTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
	public required TimeOnly EndTime { get; set; } = TimeOnly.MaxValue;
	public VisitPlanStatus Status { get; set; } = VisitPlanStatus.Active;
}
