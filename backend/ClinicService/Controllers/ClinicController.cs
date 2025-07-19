using ClinicService.Models.DTOs;
using ClinicService.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicRepository _clinicRepository;
        public ClinicController(IClinicRepository clinicRepository)
        {
                _clinicRepository = clinicRepository;
        }

        [HttpPost("AddClinic")]
        public async Task<IActionResult> AddClinic([FromBody] CreateClinicRequest createClinicRequest)
        {
            if (createClinicRequest == null)
            {
                return BadRequest("Invalid clinic data.");
            }
            var response = await _clinicRepository.AddClinic(createClinicRequest);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding clinic.");
            }
            return Ok(response);
        }
    }
}
