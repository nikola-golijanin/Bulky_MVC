using DataAccess.Repository.Categories;
using DataAccess.Repository.Companies;
using DataAccess.Repository.Products;
using DataAccess.Repository.ShoppingCarts;

namespace BulkyWeb.Configuration;

public class RepositoryServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ICompanyRepository, CompanyRepository>()
            .AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
}
