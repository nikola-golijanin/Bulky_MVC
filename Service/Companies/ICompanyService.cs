using Domain.Models;
using System.Linq.Expressions;

namespace Service.Companies;
public interface ICompanyService
{
    IEnumerable<Company> GetAll();
    IEnumerable<T> GetAllQueryable<T>(Expression<Func<Company, T>> selector);
    Company GetById(int id);
    void Create(Company company);
    void Update(Company company);
    void Delete(int id);
}
