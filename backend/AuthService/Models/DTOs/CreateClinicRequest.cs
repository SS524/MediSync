namespace AuthService.Models.DTOs
{
    public class CreateClinicRequest
    {
        public string ClinicName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }
}
