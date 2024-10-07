using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (string.Equals(category.Name, category.DisplayOrder.ToString(), StringComparison.OrdinalIgnoreCase))
            ModelState.AddModelError("Name", "Display order cannot match the Name");

        if (!ModelState.IsValid) return View();

        _context.Categories.Add(category);
        _context.SaveChanges();
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null) return NotFound();

        var category = _context.Categories.FirstOrDefault(c => c.Id == id);
        if (category is null) return NotFound();

        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid) return View();

        _context.Categories.Update(category);
        _context.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int? id)
    {
        if (id is null) return NotFound();

        var category = _context.Categories.FirstOrDefault(c => c.Id == id);
        if (category is null) return NotFound();

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteCategory(int? id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);
        if (category is null) return NotFound();
        
        _context.Categories.Remove(category);
        _context.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
