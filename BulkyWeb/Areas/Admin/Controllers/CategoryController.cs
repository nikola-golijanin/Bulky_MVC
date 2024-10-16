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
    public IActionResult Index()
    {
        var categories = _categoryService.GetAll();
        return View(categories);
    }

    public IActionResult Create() => View(new CategoryVM());

    [HttpPost]
    public IActionResult Create(CategoryVM categoryVm)
    {
        if (string.Equals(categoryVm.Name, categoryVm.DisplayOrder.ToString(), StringComparison.OrdinalIgnoreCase))
            ModelState.AddModelError("Name", "Display order cannot match the Name");

        if (!ModelState.IsValid) return View(new CategoryVM());

        var category = (Category)categoryVm;
        _categoryService.Create(category);
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        //if (id is null) return NotFound();

        var category = _categoryService.GetById(id);
        if (category is null) return NotFound();

        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid) return View();
        _categoryService.Update(category);
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }
    #endregion

    #region API
    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _categoryService.GetAll();
        return Json(new { data = categories });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _categoryService.Delete(id);
        return Ok("Category deleted successfully");
    }
    #endregion
}
