namespace DoctorService.Entities;

public class Location : BaseEntity
{
	public Guid? ParentId { get; set; } = Guid.Empty;
	public required string Title { get; set; }

	public required float Latitudes { get; set; }
	public required float Longitudes { get; set; }

	public ICollection<Clinic>? Clinics { get; set; }
}
