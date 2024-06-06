namespace DoctorService.DTOs
{
    public class ChangeDoctorLineStatusDto
    {
        public Guid Id { get; set; }
        public DoctorLineStatus LineStatus { get; set; }
    }
}