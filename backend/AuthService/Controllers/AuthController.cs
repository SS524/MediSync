using AuthService.Models;
using AuthService.Models.DTOs;
using AuthService.Services.Interface;
using Azure.Core;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Services.Interface.ITokenService _tokenService;
        private readonly IClinicServiceClient _clinicServiceClient;

        public AuthController(UserManager<ApplicationUser> userManager, 
                              SignInManager<ApplicationUser> signInManager, 
                              Services.Interface.ITokenService tokenService,
                              IClinicServiceClient clinicServiceClient)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _clinicServiceClient = clinicServiceClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest)
        {
            if (registerRequest.Role == "Admin")
            {
                if (string.IsNullOrEmpty(registerRequest.ClinicName) ||
                    string.IsNullOrEmpty(registerRequest.ClinicAddress) ||
                    string.IsNullOrEmpty(registerRequest.ClinicContactNumber))
                {
                    return BadRequest("Clinic details are required for Admin registration.");
                }
                
            }
          
            var userExists = await _userManager.FindByEmailAsync(registerRequest.Email);
            if (userExists != null)
            {
                return BadRequest("User already exists with this email.");
            }

            Gender gender;
            if (!Enum.TryParse(registerRequest.Gender, true, out gender))
            {
                return BadRequest("Invalid gender value.");
            }

            var user = new ApplicationUser
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                UserName = registerRequest.Email,
                Gender = gender,
                Role = registerRequest.Role,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            if(user.Role == "Admin")
            {
                var createClinicPayload = new CreateClinicRequest
                {
                    Address = registerRequest.ClinicAddress,
                    ClinicName = registerRequest.ClinicName,
                    ContactNumber = registerRequest.ClinicContactNumber
                };

                user.ClinicId = await _clinicServiceClient.CreateClinicAsync(createClinicPayload);
            }

            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, registerRequest.Role);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null || !user.IsActive)
            {
                return Unauthorized("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = await _tokenService.GenerateToken(user);
            return Ok(token);
        }
    }
}
