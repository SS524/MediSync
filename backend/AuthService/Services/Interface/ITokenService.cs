using AuthService.Models;
using AuthService.Models.DTOs;

namespace AuthService.Services.Interface
{
    public interface ITokenService
    {
        Task<AuthResponse> GenerateToken(ApplicationUser user);
    }
}
