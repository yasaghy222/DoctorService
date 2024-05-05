namespace DoctorService.DTOs;

public class ClinicDto
{
	public Guid? Id { get; set; }
	public required Guid LocationId { get; set; }
	public required string Address { get; set; }
	public string? Content { get; set; }
}
