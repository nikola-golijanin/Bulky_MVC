using Microsoft.AspNetCore.Identity.UI.Services;
using Service;
using Service.Categories;
using Service.Companies;
using Service.Orders;
using Service.Products;
using Service.ShoppingCarts;

namespace BulkyWeb.Configuration;

public class ApplicationServicesServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IEmailSender, EmailSenderService>()
            .AddScoped<ICompanyService, CompanyService>()
            .AddScoped<IShoppingCartService, ShoppingCartService>()
            .AddScoped<IOrderDetailService, OrderDetailService>()
            .AddScoped<IOrderHeaderService, OrderHeaderService>();
}