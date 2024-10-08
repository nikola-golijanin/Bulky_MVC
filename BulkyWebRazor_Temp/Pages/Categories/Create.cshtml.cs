using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public Category Category { get; set; }

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet()
    {
    }
}
