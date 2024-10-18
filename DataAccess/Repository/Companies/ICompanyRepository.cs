using Domain.Models;

namespace DataAccess.Repository.Companies;
public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company company);
}
