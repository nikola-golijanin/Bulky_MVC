using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public Category Category { get; set; }

    public EditModel(ApplicationDbContext context)
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
        if (!ModelState.IsValid)
            return Page();

        _context.Categories.Update(Category);
        _context.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToPage("Index");
    }
}
