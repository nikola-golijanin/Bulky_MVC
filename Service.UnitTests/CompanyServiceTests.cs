using DataAccess.Repository.Companies;
using Domain.Models;
using Moq;
using Service.Companies;
using System.Linq.Expressions;

namespace Service.UnitTests;

public class CompanyServiceTests
{
    private readonly Mock<ICompanyRepository> _companyRepositoryMock;
    private readonly CompanyService _companyService;

    public CompanyServiceTests()
    {
        _companyRepositoryMock = new Mock<ICompanyRepository>();
        _companyService = new CompanyService(_companyRepositoryMock.Object);
    }

    [Fact]
    public void Create_Should_AddCompanyToRepository()
    {
        // Arrange
        var company = new Company();

        // Act
        _companyService.Create(company);

        // Assert
        _companyRepositoryMock.Verify(repo => repo.Add(company), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveCompanyFromRepository()
    {
        // Arrange
        var companyId = 1;
        var company = new Company { Id = companyId };
        _companyRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(company);

        // Act
        await _companyService.DeleteAsync(companyId);

        // Assert
        _companyRepositoryMock.Verify(repo => repo.Remove(company), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_ThrowArgumentNullException_WhenCompanyNotFound()
    {
        // Arrange
        var companyId = 1;
        _companyRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync((Company)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _companyService.DeleteAsync(companyId));
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnAllCompaniesFromRepository()
    {
        // Arrange
        var companies = new List<Company> { new(), new() };
        _companyRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(companies);

        // Act
        var result = await _companyService.GetAllAsync();

        // Assert
        Assert.Equal(companies, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnCompanyFromRepository()
    {
        // Arrange
        var companyId = 1;
        var company = new Company { Id = companyId };
        _companyRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(company);

        // Act
        var result = await _companyService.GetByIdAsync(companyId);

        // Assert
        Assert.Equal(company, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ThrowArgumentNullException_WhenCompanyNotFound()
    {
        // Arrange
        var companyId = 1;
        _companyRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync((Company)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _companyService.GetByIdAsync(companyId));
    }
}
