using BulkyWeb.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Categories;
using Service.Products;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    #region Views
    public IActionResult Index()
    {
        var products = _productService.GetAll(including: nameof(Product.Category));
        return View(products);
    }

    public IActionResult Create()
    {
        var productVm = new ProductVM
        {
            CategoryList = GetCategoryListSelectItems(_categoryService)
        };

        return View(productVm);
    }

    [HttpPost]
    public IActionResult Create(ProductVM productVm, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            productVm.CategoryList = GetCategoryListSelectItems(_categoryService);

            return View(productVm);
        }

        _productService.Create((Product)productVm, imageFile);
        TempData["success"] = "Product created successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        //if (id is null) return NotFound();

        var product = _productService.GetById(id);

        ViewBag.CategoryList = GetCategoryListSelectItems(_categoryService);
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CategoryList = GetCategoryListSelectItems(_categoryService);
            return View();
        }

        _productService.Update(product, imageFile);

        TempData["success"] = "Product updated successfully";
        return RedirectToAction("Index");
    }
    #endregion

    #region API
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productService.GetAll(including: nameof(Product.Category));
        return Json(new { data = products });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _productService.Delete(id);
        return Ok("Product deleted successfully");
    }
    #endregion

    private static IEnumerable<SelectListItem> GetCategoryListSelectItems(ICategoryService categoryService)
        => categoryService.GetAllQueryable(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        });
}

