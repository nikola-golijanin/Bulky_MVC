using Domain.Models;

namespace DataAccess.Repository.Categories;
public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
    void SaveChanges();
}
