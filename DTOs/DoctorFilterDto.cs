namespace DoctorService.Enums;

public class DoctorFilterDto
{
	public DoctorFilterDto() { }

	public DoctorFilterDto(int pageIndex,
										  int pageSize,
										  DoctorServiceType serviceType,
										  DoctorFilterOrder order,
										  Guid? locationId)
	{
		PageIndex = pageIndex < 0 ? 1 : pageIndex;
		PageSize = pageSize < 0 ? 10 : pageSize;
		ServiceType = serviceType;
		Order = order;
		LocationId = locationId;
	}

	public int PageIndex { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public DoctorServiceType ServiceType { get; set; } = DoctorServiceType.All;
	public DoctorFilterOrder Order { get; set; } = DoctorFilterOrder.Default;
	public Guid? LocationId { get; set; }
}
