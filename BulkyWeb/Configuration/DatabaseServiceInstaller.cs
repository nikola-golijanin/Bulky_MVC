
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
	public void Install(IServiceCollection services, IConfiguration configuration)
	{
		if (configuration.GetValue<bool>("USE_INMEMORY_DB"))
			RegisterInMemoryDatabase(services);
		else
			RegisterPostgresDatabase(services, configuration);
	}

	private static void RegisterPostgresDatabase(IServiceCollection services, IConfiguration configuration) =>
		services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

	private static void RegisterInMemoryDatabase(IServiceCollection services) =>
		services
			.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb")
			.LogTo(Console.WriteLine));
}
