using DoctorService.Enums;

namespace DoctorService.DTOs;

public class OnlinePlanDto
{
	public Guid? Id { get; set; }
	public required TimeOnly StartTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
	public required TimeOnly EndTime { get; set; } = TimeOnly.MaxValue;
	public DayOfWeeks DayOfWeek { get; set; } = DayOfWeeks.Saturday;
}
