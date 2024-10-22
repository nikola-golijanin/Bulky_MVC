using DataAccess.Repository.Categories;
using DataAccess.Repository.Products;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Service.Products;
using System.Linq.Expressions;

namespace Service.UnitTests;
public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<IWebHostEnvironment> _hostingEnvironmentMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
        _productService = new ProductService(_productRepositoryMock.Object, _categoryRepositoryMock.Object, _hostingEnvironmentMock.Object);
    }

    //[Fact]
    //public void Create_Should_AddProductToRepository()
    //{
    //    // Arrange
    //    var product = new Product();
    //    var imageFileMock = new Mock<IFormFile>();
    //    var stream = new MemoryStream();
    //    var writer = new StreamWriter(stream);
    //    writer.Write("dummy image content");
    //    writer.Flush();
    //    stream.Position = 0;
    //    imageFileMock.Setup(f => f.OpenReadStream()).Returns(stream);
    //    imageFileMock.Setup(f => f.FileName).Returns("dummy.jpg");
    //    _hostingEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");

    //    // Act
    //    _productService.Create(product, imageFileMock.Object);

    //    // Assert
    //    _productRepositoryMock.Verify(repo => repo.Add(product), Times.Once);
    //    _productRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    //}

    [Fact]
    public async Task DeleteAsync_Should_RemoveProductFromRepository()
    {
        // Arrange
        var productId = 1;
        var product = new Product { Id = productId, ImageUrl = "/images/product/dummy.jpg" };
        _productRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
        _hostingEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");

        // Act
        await _productService.DeleteAsync(productId);

        // Assert
        _productRepositoryMock.Verify(repo => repo.Remove(product), Times.Once);
        _productRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_ThrowArgumentNullException_WhenProductNotFound()
    {
        // Arrange
        var productId = 1;
        _productRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _productService.DeleteAsync(productId));
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnAllProductsFromRepository()
    {
        // Arrange
        var products = new List<Product> { new(), new() };
        _productRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<string[]>())).ReturnsAsync(products);

        // Act
        var result = await _productService.GetAllAsync();

        // Assert
        Assert.Equal(products, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnProductFromRepository()
    {
        // Arrange
        var productId = 1;
        var product = new Product { Id = productId };
        _productRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);

        // Act
        var result = await _productService.GetByIdAsync(productId);

        // Assert
        Assert.Equal(product, result);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ThrowArgumentNullException_WhenProductNotFound()
    {
        // Arrange
        var productId = 1;
        _productRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _productService.GetByIdAsync(productId));
    }

    //[Fact]
    //public async Task UpdateAsync_Should_UpdateProductInRepository()
    //{
    //    // Arrange
    //    var product = new Product();
    //    var imageFileMock = new Mock<IFormFile>();
    //    var stream = new MemoryStream();
    //    var writer = new StreamWriter(stream);
    //    writer.Write("dummy image content");
    //    writer.Flush();
    //    stream.Position = 0;
    //    imageFileMock.Setup(f => f.OpenReadStream()).Returns(stream);
    //    imageFileMock.Setup(f => f.FileName).Returns("dummy.jpg");
    //    _hostingEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");

    //    // Act
    //    await _productService.UpdateAsync(product, imageFileMock.Object);

    //    // Assert
    //    _productRepositoryMock.Verify(repo => repo.Update(product), Times.Once);
    //    _productRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    //}
}