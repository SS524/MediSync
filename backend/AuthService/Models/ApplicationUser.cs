using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string ClinicId { get; set; }
        [Required]
        public string Role { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public static class UserRole 
    {
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";
        public const string SuperAdmin = "SuperAdmin";
    }
}
