using System.ComponentModel.DataAnnotations;

namespace ClinicService.Models
{
    public class Clinic
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string ClinicName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string ContactNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
