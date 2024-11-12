using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Add(T entity) => _dbSet.Add(entity);

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params string[] including)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(predicate);
        query = ApplyIncludes(query, including);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(params string[] including)
    {
        IQueryable<T> query = _dbSet;
        query = ApplyIncludes(query, including);
        return await query.ToListAsync();
    }

    public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params string[] including)
    {
        IQueryable<T> query = _dbSet;
        query = ApplyIncludes(query, including);
        return query.FirstOrDefaultAsync(predicate);
    }

    private static IQueryable<T> ApplyIncludes(IQueryable<T> query, params string[] including)
    {
        foreach (var relatedEntity in including)
            query = query.Include(relatedEntity);
        return query;
    }

    public void Remove(T entity) => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
