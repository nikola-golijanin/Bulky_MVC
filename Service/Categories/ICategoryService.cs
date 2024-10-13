using Domain.Models;
using System.Linq.Expressions;

namespace Service.Categories;
public interface ICategoryService
{
    IEnumerable<Category> GetAll();
    public IEnumerable<T> GetAllQueryable<T>(Expression<Func<Category, T>> selector);
    Category GetById(int id);
    void Create(Category category);
    void Update(Category category);
    void Delete(int id);
}
