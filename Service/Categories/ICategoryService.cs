using Domain.Models;
using System.Linq.Expressions;

namespace Service.Categories;
public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    IEnumerable<T> GetAllQueryable<T>(Expression<Func<Category, T>> selector);
    Task<Category> GetByIdAsync(int id);
    void Create(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
}
