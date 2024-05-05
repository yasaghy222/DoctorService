namespace DoctorService.Enums;

public class DoctorFilterDto
{
	public DoctorServiceType ServiceType { get; set; } = DoctorServiceType.All;
	public DoctorFilterOrder Order { get; set; } = DoctorFilterOrder.Default;
	public Guid? LocationId { get; set; }
}
