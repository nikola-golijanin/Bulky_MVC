
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        // var useOnlyInMemoryDatabase = configuration["UseOnlyInMemoryDatabase"];
        var useOnlyInMemoryDatabase = configuration.GetValue<bool>("UseOnlyInMemoryDatabase");

        if (useOnlyInMemoryDatabase)
        {
            RegisterInMemoryDatabase(services);
            return;
        }
        else RegisterPostgresDatabase(services, configuration);
    }

    private static void RegisterPostgresDatabase(IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

    private static void RegisterInMemoryDatabase(IServiceCollection services) =>
        services
            .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb")
            .LogTo(Console.WriteLine));
}
