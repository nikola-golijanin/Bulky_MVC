using DataAccess.Repository.Categories;
using DataAccess.Repository.Products;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Service.Products;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment hostingEnvironment)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _hostingEnvironment = hostingEnvironment;
    }

    public void Create(Product product, IFormFile? imageFile)
    {
        ArgumentNullException.ThrowIfNull(imageFile, nameof(imageFile));

        var wwwRootPath = _hostingEnvironment.WebRootPath;
        var filename = Guid.NewGuid().ToString() // Create a unique name
            + Path.GetExtension(imageFile.FileName); // Get the extension of the file

        var productImagesDirPath = Path.Combine(wwwRootPath, @"images\product");

        using var fileStream = new FileStream(Path.Combine(productImagesDirPath, filename), FileMode.Create);
        imageFile.CopyTo(fileStream);
        product.ImageUrl = @"\images\product\" + filename;

        _productRepository.Add(product);
        _productRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetFirstOrDefaultAsync(p => p.Id == id);
        ArgumentNullException.ThrowIfNull(product, nameof(product));

        var imagePath = _hostingEnvironment.WebRootPath + product.ImageUrl;
        DeleteProductImage(imagePath);

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync();
    }

    public Task<IEnumerable<Product>> GetAllAsync(params string[] including) => _productRepository.GetAllAsync(including);

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetFirstOrDefaultAsync(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(product, nameof(product));
        return product;
    }

    public async Task UpdateAsync(Product product, IFormFile? imageFile)
    {
        if (imageFile is not null)
        {
            var wwwRootPath = _hostingEnvironment.WebRootPath;
            var filename = Guid.NewGuid().ToString() // Create a unique name
                + Path.GetExtension(imageFile.FileName); // Get the extension of the file

            var productImagesDirPath = Path.Combine(wwwRootPath, @"images\product");

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = wwwRootPath + product.ImageUrl;
                DeleteProductImage(imagePath);
            }

            using var fileStream = new FileStream(Path.Combine(productImagesDirPath, filename), FileMode.Create);
            imageFile.CopyTo(fileStream);
            product.ImageUrl = @"\images\product\" + filename;
        }
        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
    }

    private static void DeleteProductImage(string imagePath)
    {
        if (File.Exists(imagePath))
            File.Delete(imagePath);
    }
}
