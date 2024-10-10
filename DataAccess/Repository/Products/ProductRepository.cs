using DataAccess.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository.Products;
public class ProductRepository : Repository<Product>, IProductRepository
{
	private readonly ApplicationDbContext _context;

	public ProductRepository(ApplicationDbContext context) : base(context)
	{
		_context = context;
	}

	public IEnumerable<Product> GetAllIncluding<TProperty>(Expression<Func<Product, TProperty>> includeProperty)
		=> _context.Products
			.Include(includeProperty)
			.ToList();

	public void Update(Product product) => _context.Products.Update(product);
}
