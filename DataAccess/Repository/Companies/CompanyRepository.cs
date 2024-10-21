using DataAccess.Data;
using Domain.Models;
using System.Linq.Expressions;

namespace DataAccess.Repository.Companies;
public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private readonly ApplicationDbContext _context;
    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAllQueryable<T>(Expression<Func<Company, T>> selector)
        => _context.Companies
                    .Select(selector)
                    .ToList();

    public void Update(Company company) => _context.Companies.Update(company);
}
