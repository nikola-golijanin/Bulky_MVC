using BulkyWeb.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Categories;
using Utility;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    #region Views
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

    public IActionResult Create() => View(new CategoryVM());

    [HttpPost]
    public async Task<IActionResult> Create(CategoryVM categoryVm)
    {
        if (string.Equals(categoryVm.Name, categoryVm.DisplayOrder.ToString(), StringComparison.OrdinalIgnoreCase))
            ModelState.AddModelError("Name", "Display order cannot match the Name");

        if (!ModelState.IsValid) return View(new CategoryVM());

        var category = (Category)categoryVm;
        await _categoryService.Create(category);
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        //if (id is null) return NotFound();

        var category = await _categoryService.GetByIdAsync(id);
        if (category is null) return NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (!ModelState.IsValid) return View();
        await _categoryService.UpdateAsync(category);
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }
    #endregion

    #region API
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Json(new { data = categories });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id);
        return Ok("Category deleted successfully");
    }
    #endregion
}
