namespace BulkyWeb.Configuration;

public class PresentationServiceInstaller : IServiceInstaller
{
	public void Install(IServiceCollection services, IConfiguration configuration)
	{
		services.AddRazorPages();
		services.AddControllersWithViews();
	}
}