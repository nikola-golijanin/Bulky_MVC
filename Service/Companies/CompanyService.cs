using DataAccess.Repository.Companies;
using Domain.Models;

namespace Service.Companies;
public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public void Create(Company company)
    {
        _companyRepository.Add(company);
        _companyRepository.SaveChanges();
    }

    public void Delete(int id)
    {
        var company = _companyRepository.GetFirstOrDefault(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(company, nameof(company));
        _companyRepository.Remove(company);
        _companyRepository.SaveChanges();
    }

    public IEnumerable<Company> GetAll() => _companyRepository.GetAll();

    public Company GetById(int id)
    {
        var company = _companyRepository.GetFirstOrDefault(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(company, nameof(company));
        return company;
    }

    public void Update(Company company)
    {
        _companyRepository.Update(company);
        _companyRepository.SaveChanges();
    }
}
