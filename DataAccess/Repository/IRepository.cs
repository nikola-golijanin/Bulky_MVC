using System.Linq.Expressions;

namespace DataAccess.Repository;

public interface IRepository<T> where T : class
{
	IEnumerable<T> GetAll();
	T? GetFirstOrDefault(Expression<Func<T, bool>> predicate);
	void Add(T entity);
	void Remove(T entity);
	void RemoveRange(IEnumerable<T> entities);
	void SaveChanges();
}
