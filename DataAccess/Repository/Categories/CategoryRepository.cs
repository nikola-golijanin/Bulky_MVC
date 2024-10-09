using DataAccess.Data;
using Domain.Models;

namespace DataAccess.Repository.Categories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void SaveChanges() => _context.SaveChanges();

    public void Update(Category category) => _context.Categories.Update(category);
}
