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


        var cartList = await _shoppingCartService.GetAllShoppingCartsForUserAsync(userId);
        var totalOrderPirce = _shoppingCartService.CalculateTotalOrderPrice(cartList);

        ShoppingCartVM = new ShoppingCartVM
        {
            CartList = cartList,
            OrderTotal = totalOrderPirce
        };

        return View(ShoppingCartVM);
    }

    public IActionResult Summary() => View();

    public async Task<IActionResult> Plus(int cartId)
    {
        await _shoppingCartService.IncrementProductCountAsync(cartId);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Minus(int cartId)
    {
        await _shoppingCartService.DecrementProductCount(cartId);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(int cartId)
    {
        await _shoppingCartService.RemoveShoppingCartAsync(cartId);
        return RedirectToAction(nameof(Index));
    }

}
