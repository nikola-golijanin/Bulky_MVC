using DataAccess.Data;
using Domain.Models;
using System.Linq.Expressions;

namespace DataAccess.Repository.Categories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
	private readonly ApplicationDbContext _context;

	public CategoryRepository(ApplicationDbContext context) : base(context)
	{
		_context = context;
	}

	public IEnumerable<T> GetAllQueryable<T>(Expression<Func<Category, T>> selector)
		=> _context.Categories
					.Select(selector)
					.ToList();

	public void Update(Category category) => _context.Categories.Update(category);
}
