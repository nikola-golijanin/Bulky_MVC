using DataAccess.Repository.Categories;
using Domain.Models;
using System.Linq.Expressions;

namespace Service.Categories;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void Create(Category category)
    {
        _categoryRepository.Add(category);
        _categoryRepository.SaveChanges();
    }

    public IEnumerable<T> GetAllQueryable<T>(Expression<Func<Category, T>> selector) => _categoryRepository.GetAllQueryable(selector);

    public void Delete(int id)
    {
        var category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(category, nameof(category));
        _categoryRepository.Remove(category);
        _categoryRepository.SaveChanges();
    }

    public IEnumerable<Category> GetAll() => _categoryRepository.GetAll();

    public Category GetById(int id)
    {
        var category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(category, nameof(category));
        return category;
    }

    public void Update(Category category)
    {
        _categoryRepository.Update(category);
        _categoryRepository.SaveChanges();
    }
}
