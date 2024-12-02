using BulkyWeb.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.ShoppingCarts;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class CartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;
    private readonly UserManager<ApplicationUser> _userManager;

    [BindProperty]
    public ShoppingCartVM ShoppingCartVM { get; set; }

    public CartController(
        IShoppingCartService shoppingCartService,
        UserManager<ApplicationUser> userManager)
    {
        _shoppingCartService = shoppingCartService;
        _userManager = userManager;
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
            OrderHeader = new OrderHeader
            {
                OrderTotal = totalOrderPirce
            }
        };

        return View(ShoppingCartVM);
    }

    public async Task<IActionResult> Summary()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        var applicationUser = await _userManager.FindByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(applicationUser, nameof(applicationUser));

        var cartList = await _shoppingCartService.GetAllShoppingCartsForUserAsync(userId);
        var totalOrderPirce = _shoppingCartService.CalculateTotalOrderPrice(cartList);

        ShoppingCartVM = new ShoppingCartVM
        {
            CartList = cartList,
            OrderHeader = PopulateOrderHeaderFromUserData(applicationUser),
        };

        ShoppingCartVM.OrderHeader.OrderTotal = totalOrderPirce;

        return View(ShoppingCartVM);

        static OrderHeader PopulateOrderHeaderFromUserData(ApplicationUser user) => new()
        {
            ApplicationUser = user,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            StreetAddress = user.StreetAddress,
            City = user.City,
            State = user.State,
            ZipCode = user.ZipCode
        };
    }

    [HttpPost]
    [ActionName(nameof(Summary))]
    public async Task<IActionResult> SummarySubmit()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        var applicationUser = await _userManager.FindByIdAsync(userId);
        ArgumentNullException.ThrowIfNull(applicationUser, nameof(applicationUser));

        var cartList = await _shoppingCartService.GetAllShoppingCartsForUserAsync(userId);
        var totalOrderPirce = _shoppingCartService.CalculateTotalOrderPrice(cartList);

        ShoppingCartVM.CartList = cartList;

        await _shoppingCartService.CreateOrderHeader(
            orderHeader: ShoppingCartVM.OrderHeader,
            user: applicationUser,
            totalPrice: totalOrderPirce);

        await _shoppingCartService.CreateOrderDetails(cartList, ShoppingCartVM.OrderHeader);
        return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id });
    }

    public IActionResult OrderConfirmation(int id)
    {
        return View(id);
    }
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
