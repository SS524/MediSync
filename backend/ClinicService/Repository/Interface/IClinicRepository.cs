using ClinicService.Models.DTOs;

namespace ClinicService.Repository.Interface
{
    public interface IClinicRepository
    {
        Task<CreateClinicResponse> AddClinic(CreateClinicRequest createClinicRequest);
    }
}
