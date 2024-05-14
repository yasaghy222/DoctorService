namespace DoctorService;

public class SpecialtyInfo
{
	public Guid Id { get; set; }
	public required string Title { get; set; }
	public string? Content { get; set; }
	public Guid[]? DoctorIds { get; set; }
	public required string ImagePath { get; set; }
}
