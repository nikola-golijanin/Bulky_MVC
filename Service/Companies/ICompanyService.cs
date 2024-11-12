using Domain.Models;
using System.Linq.Expressions;

namespace Service.Companies;
public interface ICompanyService
{
    Task<IEnumerable<Company>> GetAllAsync();

    IEnumerable<T> GetAllQueryable<T>(Expression<Func<Company, T>> selector);

    Task<Company> GetByIdAsync(int id);

    Task Create(Company company);

    Task UpdateAsync(Company company);

    Task DeleteAsync(int id);
}
