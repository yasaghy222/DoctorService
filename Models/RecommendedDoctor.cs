namespace DoctorService.Models;

public class RecommendedDoctor
{
	public Guid Id { get; set; }
	public required string FullName { get; set; }
	public required string SpecialtyTitle { get; set; }
	public int SuccessConsolationCount { get; set; }
	public float Rate { get; set; }

	public DoctorLineStatus LineStatus { get; set; }
}
