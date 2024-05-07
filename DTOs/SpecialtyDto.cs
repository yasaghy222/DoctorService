namespace DoctorService.DTOs;

public class SpecialtyDto
{
	public Guid? Id { get; set; }
	public required string Title { get; set; }
	public string? Content { get; set; }
}
