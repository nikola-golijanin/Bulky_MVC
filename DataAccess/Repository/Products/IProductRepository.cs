using Domain.Models;

namespace DataAccess.Repository.Products;

public interface IProductRepository : IRepository<Product>
{
	void Update(Product product);
}
