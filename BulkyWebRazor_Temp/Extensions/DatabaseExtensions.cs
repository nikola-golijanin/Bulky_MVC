using BulkyWebRazor_Temp.Data;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor_Temp.Extensions;

public static class DatabaseExtensions
{
    public static void RegisterPostgresDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }

    public static void RegisterInMemoryDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
    }

    public static void ApplyMigrationsToInMemoryDB(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }
}
