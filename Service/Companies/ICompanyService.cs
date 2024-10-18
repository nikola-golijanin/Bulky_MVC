using Domain.Models;

namespace Service.Companies;
public interface ICompanyService
{
    IEnumerable<Company> GetAll();
    Company GetById(int id);
    void Create(Company company);
    void Update(Company company);
    void Delete(int id);
}
