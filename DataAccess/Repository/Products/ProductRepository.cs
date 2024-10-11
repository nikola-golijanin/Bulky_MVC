using DataAccess.Data;
using Domain.Models;

namespace DataAccess.Repository.Products;
public class ProductRepository : Repository<Product>, IProductRepository
{
	private readonly ApplicationDbContext _context;

	public ProductRepository(ApplicationDbContext context) : base(context)
	{
		_context = context;
	}

	public void Update(Product product) => _context.Products.Update(product);
}
