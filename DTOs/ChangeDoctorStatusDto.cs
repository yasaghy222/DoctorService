namespace DoctorService.DTOs
{
    public class ChangeDoctorStatusDto
    {
        public Guid Id { get; set; }
        public DoctorStatus Status { get; set; }
        public string? StatusDescription { get; set; }
    }
}