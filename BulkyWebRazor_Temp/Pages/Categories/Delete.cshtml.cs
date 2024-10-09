using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public Category Category { get; set; }

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet(int? id)
    {
        if (id is not null)
            Category = _context.Categories.FirstOrDefault(c => c.Id == id);
    }

    public IActionResult OnPost()
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == Category.Id);
        if (category is null) return NotFound();

        _context.Categories.Remove(category);
        _context.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToPage("Index");
    }
}
