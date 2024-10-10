using Domain.Models;
using System.Linq.Expressions;

namespace DataAccess.Repository.Products;

public interface IProductRepository : IRepository<Product>
{
	IEnumerable<Product> GetAllIncluding<TProperty>(Expression<Func<Product, TProperty>> includeProperty);
	void Update(Product product);
}
