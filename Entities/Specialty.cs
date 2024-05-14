namespace DoctorService.Entities
{
    public class Specialty : BaseEntity
    {
        public required string Title { get; set; }
        public string? Content { get; set; }
        public required string ImagePath { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    }
}