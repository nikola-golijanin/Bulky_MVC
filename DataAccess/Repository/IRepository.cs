using System.Linq.Expressions;

namespace DataAccess.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params string[] including);

    Task<IEnumerable<T>> GetAllAsync(params string[] including);

    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params string[] including);

    void Add(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task SaveChangesAsync();
}
