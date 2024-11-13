using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Service.Products;
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsIncludingCategories();

    Task<Product> GetByIdAsync(int id);

    Task CreateAsync(Product product, IFormFile? imageFile);

    Task UpdateAsync(Product product, IFormFile? imageFile);

    Task DeleteAsync(int id);
}
