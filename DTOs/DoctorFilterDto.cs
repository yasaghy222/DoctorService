namespace DoctorService.Enums;

public class DoctorFilterDto
{
	public DoctorFilterDto() { }

	public DoctorFilterDto(DoctorServiceType serviceType,
											DoctorFilterOrder order,
											Guid? locationId)
	{
		ServiceType = serviceType;
		Order = order;
		LocationId = locationId;
	}

	public DoctorServiceType ServiceType { get; set; } = DoctorServiceType.All;
	public DoctorFilterOrder Order { get; set; } = DoctorFilterOrder.Default;
	public Guid? LocationId { get; set; }
}
