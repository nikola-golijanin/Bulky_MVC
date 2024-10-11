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

	public IEnumerable<T> GetAll(params string[] including)
	{
		IQueryable<T> query = _dbSet;
		foreach (var relatedEntity in including)
			query = query.Include(relatedEntity);
		return query.ToList();
	}

	public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] including)
	{
		IQueryable<T> query = _dbSet;
		foreach (var relatedEntity in including)
			query = query.Include(relatedEntity);

		return query.FirstOrDefault(predicate);
	}

	public void Remove(T entity) => _dbSet.Remove(entity);

	public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

	public void SaveChanges() => _context.SaveChanges();
}
