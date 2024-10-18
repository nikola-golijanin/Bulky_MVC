using DataAccess.Data;
using Domain.Models;

namespace DataAccess.Repository.Companies;
public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private readonly ApplicationDbContext _context;
    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Company company) => _context.Companies.Update(company);
}
