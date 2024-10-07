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
        //if (string.Equals(category.Name, category.DisplayOrder.ToString(), StringComparison.OrdinalIgnoreCase))
        //    ModelState.AddModelError("Name", "Display order cannot match the Name");

        if (!ModelState.IsValid) return View();

        _context.Categories.Add(category);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

}
