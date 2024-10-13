using BulkyWeb.ViewModels;
using DataAccess.Repository.Categories;
using DataAccess.Repository.Products;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment hostingEnvironment)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _hostingEnvironment = hostingEnvironment;
    }

    #region Views
    public IActionResult Index()
    {
        var products = _productRepository.GetAll(including: nameof(Product.Category));
        return View(products);
    }

    public IActionResult Create()
    {
        var productVm = new ProductVM
        {
            CategoryList = GetCategoryListSelectItems(_categoryRepository)
        };

        return View(productVm);
    }

    [HttpPost]
    public IActionResult Create(ProductVM productVm, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            productVm.CategoryList = GetCategoryListSelectItems(_categoryRepository);
            return View(productVm);
        }

        if (imageFile is not null)
        {
            var wwwRootPath = _hostingEnvironment.WebRootPath;
            var filename = Guid.NewGuid().ToString() // Create a unique name
                + Path.GetExtension(imageFile.FileName); // Get the extension of the file

            var productImagesDirPath = Path.Combine(wwwRootPath, @"images\product");

            using var fileStream = new FileStream(Path.Combine(productImagesDirPath, filename), FileMode.Create);
            imageFile.CopyTo(fileStream);
            productVm.ImageUrl = @"\images\product\" + filename;
        }
        var product = (Product)productVm;
        _productRepository.Add(product);
        _productRepository.SaveChanges();
        TempData["success"] = "Product created successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null) return NotFound();

        var product = _productRepository.GetFirstOrDefault(c => c.Id == id);
        if (product is null) return NotFound();

        ViewBag.CategoryList = GetCategoryListSelectItems(_categoryRepository);
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CategoryList = GetCategoryListSelectItems(_categoryRepository);
            return View();
        }
        if (imageFile is not null)
        {
            var wwwRootPath = _hostingEnvironment.WebRootPath;
            var filename = Guid.NewGuid().ToString() // Create a unique name
                + Path.GetExtension(imageFile.FileName); // Get the extension of the file

            var productImagesDirPath = Path.Combine(wwwRootPath, @"images\product");

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = wwwRootPath + product.ImageUrl;
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            using var fileStream = new FileStream(Path.Combine(productImagesDirPath, filename), FileMode.Create);
            imageFile.CopyTo(fileStream);
            product.ImageUrl = @"\images\product\" + filename;
        }
        _productRepository.Update(product);
        _productRepository.SaveChanges();
        TempData["success"] = "Product updated successfully";
        return RedirectToAction("Index");
    }
    #endregion

    #region API
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productRepository.GetAll(including: nameof(Product.Category));
        return Json(new { data = products });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var product = _productRepository.GetFirstOrDefault(c => c.Id == id);
        if (product is null) return NotFound();

        // Delete the image
        var imagePath = _hostingEnvironment.WebRootPath + product.ImageUrl;
        if (System.IO.File.Exists(imagePath))
            System.IO.File.Delete(imagePath);

        _productRepository.Remove(product);
        _productRepository.SaveChanges();
        return Ok("Product deleted successfully");
    }
    #endregion

    private static IEnumerable<SelectListItem> GetCategoryListSelectItems(ICategoryRepository categoryRepository)
        => categoryRepository.GetAllQueryable(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        });
}

