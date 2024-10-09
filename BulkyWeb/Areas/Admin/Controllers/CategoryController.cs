using DataAccess.Repository.Categories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index()
    {
        var categories = _categoryRepository.GetAll();
        return View(categories);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (string.Equals(category.Name, category.DisplayOrder.ToString(), StringComparison.OrdinalIgnoreCase))
            ModelState.AddModelError("Name", "Display order cannot match the Name");

        if (!ModelState.IsValid) return View();

        _categoryRepository.Add(category);
        _categoryRepository.SaveChanges();
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null) return NotFound();

        var category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
        if (category is null) return NotFound();

        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid) return View();

        _categoryRepository.Update(category);
        _categoryRepository.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int? id)
    {
        if (id is null) return NotFound();

        var category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
        if (category is null) return NotFound();

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteCategory(int? id)
    {

        var category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
        if (category is null) return NotFound();

        _categoryRepository.Remove(category);
        _categoryRepository.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
