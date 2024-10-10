using Domain.Models;
using System.Linq.Expressions;

namespace DataAccess.Repository.Categories;
public interface ICategoryRepository : IRepository<Category>
{
	void Update(Category category);
	IEnumerable<T> GetAllQueryable<T>(Expression<Func<Category, T>> selector);
}
