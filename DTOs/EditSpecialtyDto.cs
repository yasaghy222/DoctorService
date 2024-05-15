namespace DoctorService.DTOs;

public class EditSpecialtyDto
{
	public IFormFile? Image { get; set; }
	public Guid Id { get; set; }
	public required string Title { get; set; }
	public string? Content { get; set; }
}
