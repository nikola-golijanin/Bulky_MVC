
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;

namespace BulkyWeb.Configuration;

public class IdentityServiceInstaller : IServiceInstaller
{
	public void Install(IServiceCollection services, IConfiguration configuration)
	{
		services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
	}
}
