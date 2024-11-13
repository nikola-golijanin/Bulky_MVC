using DataAccess.Repository.Products;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ShoppingCarts;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IShoppingCartService _shoppingCartService;

    public HomeController(
        ILogger<HomeController> logger,
        IProductRepository productRepository,
        IShoppingCartService shoppingCartService)
    {
        _logger = logger;
        _productRepository = productRepository;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync(including: nameof(Product.Category));
        return View(products);
    }

    public async Task<IActionResult> Details(int productId)
    {

        var product = await _productRepository.GetFirstOrDefaultAsync(p => p.Id == productId, including: nameof(Product.Category));
        ArgumentNullException.ThrowIfNull(product, nameof(product));

        var shoppingCart = new ShoppingCart
        {
            Product = product,
            ProductId = productId,
            Count = 1
        };
        return View(shoppingCart);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Details(ShoppingCart shoppingCart)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));
        shoppingCart.ApplicationUserId = userId;

        var cartFromDb = await _shoppingCartService.GetShoppingCartForUserAsync(userId, shoppingCart.ProductId);

        if (cartFromDb is not null)
        {
            await _shoppingCartService.UpdateShoppingCart(cartFromDb, shoppingCart);
        }
        else
        {
            await _shoppingCartService.CreateNewShoppingCart(shoppingCart);
        }

        TempData["success"] = "Cart updated successfully";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
