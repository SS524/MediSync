namespace AuthService.Models.DTOs
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; } // Optional, for future enhancement
        public DateTime ExpiresAt { get; set; }
    }
}
