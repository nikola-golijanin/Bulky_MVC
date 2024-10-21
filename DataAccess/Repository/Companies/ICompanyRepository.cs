using Domain.Models;
using System.Linq.Expressions;

namespace DataAccess.Repository.Companies;
public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company company);
    IEnumerable<T> GetAllQueryable<T>(Expression<Func<Company, T>> selector);
}
