using AuthService.Config;
using AuthService.Data;
using AuthService.Models;
using AuthService.Seeders;
using AuthService.Services;
using AuthService.Services.External;
using AuthService.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryClients(IdentityServerConfig.Clients)
    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    .AddDeveloperSigningCredential();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<SuperAdminSeeder>();


// Registering HttpClient for calling ClinicService
var clinicServiceUrl = builder.Configuration["ServiceUrls:ClinicService"];

builder.Services.AddHttpClient<IClinicServiceClient, ClinicServiceClient>(client =>
{
    client.BaseAddress = new Uri(clinicServiceUrl);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
}

using (var scope = app.Services.CreateScope())
{
    var superAdminSeeder = scope.ServiceProvider.GetRequiredService<SuperAdminSeeder>();
    await superAdminSeeder.SeedSuperAdminAsync();
}

app.Run();


