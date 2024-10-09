using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("USE_INMEMORY_DB"))
            services.RegisterInMemoryDatabase();
        else
            services.RegisterPostgresDatabase(configuration);

        return services;
    }

    private static void RegisterPostgresDatabase(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

    private static void RegisterInMemoryDatabase(this IServiceCollection services)
        => services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

    public static void ApplyMigrationsToInMemoryDB(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }
}
