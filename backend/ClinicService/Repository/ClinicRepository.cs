using ClinicService.Data;
using ClinicService.Models;
using ClinicService.Models.DTOs;
using ClinicService.Repository.Interface;

namespace ClinicService.Repository
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ClinicDbContext _clinicDbContext;
        public ClinicRepository(ClinicDbContext clinicDbContext)
        {
            _clinicDbContext = clinicDbContext;

        }
        public async Task<CreateClinicResponse> AddClinic(CreateClinicRequest createClinicRequest)
        {
            var clinicObj = new Clinic
            {
                ClinicName = createClinicRequest.ClinicName,
                Address = createClinicRequest.Address,
                ContactNumber = createClinicRequest.ContactNumber
            };
            await _clinicDbContext.Clinics.AddAsync(clinicObj);
            await _clinicDbContext.SaveChangesAsync();
            return new CreateClinicResponse
            {
                ClinicId = clinicObj.Id,
               
            };

        }
    }
}
