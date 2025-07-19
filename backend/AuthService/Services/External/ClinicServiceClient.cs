using AuthService.Models.DTOs;
using AuthService.Services.Interface;
using Duende.IdentityServer.Validation;

namespace AuthService.Services.External
{
    public class ClinicServiceClient : IClinicServiceClient
    {
        private readonly HttpClient _httpClient;
        public ClinicServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> CreateClinicAsync(CreateClinicRequest createClinicRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Clinic/AddClinic", createClinicRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create clinic");
            }

            var result = await response.Content.ReadFromJsonAsync<CreateClinicResponse>();
            return result.ClinicId;

        }

    }
}

