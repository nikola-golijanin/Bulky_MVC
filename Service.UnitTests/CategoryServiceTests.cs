using DataAccess.Repository.Categories;
using Domain.Models;
using Moq;
using Service.Categories;
using System.Linq.Expressions; // Add this using directive at the top of the file


namespace Service.UnitTests;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly CategoryService _categoryService;

    public CategoryServiceTests()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();

        // Rest of the file remains unchanged
        _categoryService = new CategoryService(_categoryRepositoryMock.Object);
    }

    [Fact]
    public void Create_Should_AddCategoryToRepository()
    {
        // Arrange
        var category = new Category();

        // Act
        _categoryService.Create(category);

        // Assert
        _categoryRepositoryMock.Verify(repo => repo.Add(category), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveCategoryFromRepository()
    {
        // Arrange
        var categoryId = 1;
        var category = new Category { Id = categoryId };
        _categoryRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);

        // Act
        await _categoryService.DeleteAsync(categoryId);

        // Assert
        _categoryRepositoryMock.Verify(repo => repo.Remove(category), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_ThrowArgumentNullException_WhenCategoryNotFound()
    {
        // Arrange
        var categoryId = 1;
        _categoryRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync((Category)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _categoryService.DeleteAsync(categoryId));
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnAllCategoriesFromRepository()
    {
        // Arrange
        var categories = new List<Category> { new(), new() };
        _categoryRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

        // Act
        var result = await _categoryService.GetAllAsync();

        // Assert
        Assert.Equal(categories, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnCategoryFromRepository()
    {
        // Arrange
        var categoryId = 1;
        var category = new Category { Id = categoryId };
        _categoryRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(category);

        // Act
        var result = await _categoryService.GetByIdAsync(categoryId);

        // Assert
        Assert.Equal(category, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ThrowArgumentNullException_WhenCategoryNotFound()
    {
        // Arrange
        var categoryId = 1;
        _categoryRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync((Category)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _categoryService.GetByIdAsync(categoryId));
    }
}