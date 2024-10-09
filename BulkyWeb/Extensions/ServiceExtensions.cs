using DataAccess.Repository.Categories;
using DataAccess.Repository.Products;

namespace BulkyWeb.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
