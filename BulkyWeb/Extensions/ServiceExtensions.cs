using DataAccess.Repository.Categories;

namespace BulkyWeb.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
}
