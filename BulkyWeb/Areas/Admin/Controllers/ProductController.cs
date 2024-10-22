using BulkyWeb.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Categories;
using Service.Products;
using Utility;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
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
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync(including: nameof(Product.Category));
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

    public async Task<IActionResult> Edit(int id)
    {
        //if (id is null) return NotFound();

        var product = await _productService.GetByIdAsync(id);

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

        _productService.UpdateAsync(product, imageFile);

        TempData["success"] = "Product updated successfully";
        return RedirectToAction("Index");
    }
    #endregion

    #region API
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync(including: nameof(Product.Category));
        return Json(new { data = products });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
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

