using AuthService.Models.DTOs;

namespace AuthService.Services.Interface
{
    public interface IClinicServiceClient
    {
        Task<string> CreateClinicAsync(CreateClinicRequest createClinicRequest);
    }
}
