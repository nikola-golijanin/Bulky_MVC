using System.Linq.Expressions;

namespace DataAccess.Repository;

public interface IRepository<T> where T : class
{
	IEnumerable<T> GetAll(params string[] including);
	T? GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] including);
	void Add(T entity);
	void Remove(T entity);
	void RemoveRange(IEnumerable<T> entities);
	void SaveChanges();
}
