using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Service.Products;
public interface IProductService
{
    IEnumerable<Product> GetAll(params string[] including);
    Product GetById(int id);
    void Create(Product product, IFormFile? imageFile);
    void Update(Product product, IFormFile? imageFile);
    void Delete(int id);
}
