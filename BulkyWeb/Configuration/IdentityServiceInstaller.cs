
using DataAccess.Data;
using Domain.Models;

namespace BulkyWeb.Configuration;

public class IdentityServiceInstaller : IServiceInstaller
{
	public void Install(IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddDefaultIdentity<ApplicationUser>()
			.AddEntityFrameworkStores<ApplicationDbContext>();
	}
}
