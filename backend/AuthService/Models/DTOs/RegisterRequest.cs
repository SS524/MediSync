namespace AuthService.Models.DTOs
{
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Patient", "Admin"
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string ClinicContactNumber { get; set; }
    }
}
