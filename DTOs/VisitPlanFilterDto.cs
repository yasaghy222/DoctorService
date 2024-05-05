namespace DoctorService;

public class VisitPlanFilterDto
{
	public required Guid DoctorId { get; set; }
	public DateOnly FromDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	public DateOnly ToDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(7));
}
