using BulkyWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ShoppingCarts;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class CartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartVM ShoppingCartVM { get; set; }

    public CartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        ShoppingCartVM = new ShoppingCartVM
        {
            CartList = await _shoppingCartService.GetAllShoppingCartsForUser(userId),
        };
        return View(ShoppingCartVM);
    }
}
