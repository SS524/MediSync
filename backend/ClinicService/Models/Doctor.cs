using System.ComponentModel.DataAnnotations;

namespace ClinicService.Models
{
    public class Doctor
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Specialization { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string ClinicId { get; set; }

        // Navigation property
        public Clinic Clinic { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
