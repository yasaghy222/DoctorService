namespace DoctorService;

public class VisitPlanFilterDto
{
	public VisitPlanFilterDto() { }

	public VisitPlanFilterDto(Guid doctorId, DateOnly fromDate, DateOnly toDate)
	{
		DoctorId = doctorId;
		FromDate = fromDate;
		ToDate = toDate;
	}

	public Guid DoctorId { get; set; }
	public DateOnly FromDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	public DateOnly ToDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(7));
}
