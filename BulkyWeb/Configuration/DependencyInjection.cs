using DataAccess.Data;
using System.Reflection;

namespace BulkyWeb.Configuration;

public static class DependencyInjection
{
	public static IServiceCollection InstallServices(
		this IServiceCollection services,
		IConfiguration configuration,
		params Assembly[] assemblies)
	{
		IEnumerable<IServiceInstaller> serviceInstallers = assemblies
			.SelectMany(a => a.DefinedTypes)
			.Where(IsAssignableToType<IServiceInstaller>)
			.Select(Activator.CreateInstance)
			.Cast<IServiceInstaller>()
			.ToList();

		foreach (var serviceInstaller in serviceInstallers)
			serviceInstaller.Install(services, configuration);

		return services;


		static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
			typeof(T).IsAssignableFrom(typeInfo) &&
			!typeInfo.IsInterface &&
			!typeInfo.IsAbstract;
	}

	public static void ApplyMigrationsToInMemoryDB(this IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		context.Database.EnsureCreated();
	}
}
