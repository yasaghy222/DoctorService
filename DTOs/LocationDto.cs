namespace DoctorService.DTOs;

public class LocationDto
{
	public Guid? Id { get; set; }
	public Guid? ParentId { get; set; } = Guid.Empty;
	public required string Title { get; set; }

	public required float Latitudes { get; set; }
	public required float Longitudes { get; set; }
}
