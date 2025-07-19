using AuthService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Seeders
{
    public class SuperAdminSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SuperAdminSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedSuperAdminAsync()
        {
            string superAdminEmail = "superadmin@medisync.com";
            string password = "SuperAdmin@123";

            // Create Role if not exists
            if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            // Check if user exists
            var user = await _userManager.FindByEmailAsync(superAdminEmail);
            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    FullName = "Super Admin",
                    Email = superAdminEmail,
                    UserName = superAdminEmail,
                    Gender = Gender.Male,
                    Role = UserRole.SuperAdmin,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "SuperAdmin");
                }
                else
                {
                    throw new Exception("SuperAdmin user creation failed.");
                }
            }
        }
    }
}
