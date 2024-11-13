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

    public async Task CreateAsync(Category category)
    {
        _categoryRepository.Add(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public IEnumerable<T> GetAllQueryable<T>(Expression<Func<Category, T>> selector) => _categoryRepository.GetAllQueryable(selector);

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetFirstOrDefaultAsync(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(category, nameof(category));
        _categoryRepository.Remove(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public Task<IEnumerable<Category>> GetAllAsync() => _categoryRepository.GetAllAsync();

    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetFirstOrDefaultAsync(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(category, nameof(category));
        return category;
    }

    public async Task UpdateAsync(Category category)
    {
        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();
    }
}
