
using DataAccess.Data;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BulkyWeb.Configuration;

public class IdentityServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        //Configure Application Cookie must go after AddIdentity
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });
    }
}
