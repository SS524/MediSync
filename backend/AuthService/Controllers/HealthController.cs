using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Status = "Healthy" });
        }
    }
}
