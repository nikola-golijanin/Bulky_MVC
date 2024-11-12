using DataAccess.Repository.Companies;
using Domain.Models;
using System.Linq.Expressions;

namespace Service.Companies;
public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Create(Company company)
    {
        _companyRepository.Add(company);
        await _companyRepository.SaveChangesAsync();
    }
    public IEnumerable<T> GetAllQueryable<T>(Expression<Func<Company, T>> selector) => _companyRepository.GetAllQueryable(selector);

    public async Task DeleteAsync(int id)
    {
        var company = await _companyRepository.GetFirstOrDefaultAsync(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(company, nameof(company));
        _companyRepository.Remove(company);
        await _companyRepository.SaveChangesAsync();
    }

    public Task<IEnumerable<Company>> GetAllAsync() => _companyRepository.GetAllAsync();

    public async Task<Company> GetByIdAsync(int id)
    {
        var company = await _companyRepository.GetFirstOrDefaultAsync(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(company, nameof(company));
        return company;
    }

    public async Task UpdateAsync(Company company)
    {
        _companyRepository.Update(company);
        await _companyRepository.SaveChangesAsync();
    }
}
