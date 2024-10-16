using Service.Categories;
using Service.Products;

namespace BulkyWeb.Configuration;

public class ApplicationServicesServiceInstaller : IServiceInstaller
{
	public void Install(IServiceCollection services, IConfiguration configuration) =>
		services
			.AddScoped<ICategoryService, CategoryService>()
			.AddScoped<IProductService, ProductService>();
}