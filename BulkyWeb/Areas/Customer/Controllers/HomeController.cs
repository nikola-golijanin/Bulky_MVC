using DataAccess.Repository.Products;
using DataAccess.Repository.ShoppingCarts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _productRepository;

    // TODO Add IShoppingService
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _shoppingCartRepository = shoppingCartRepository;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync(including: nameof(Product.Category));
        return View(products);
    }

    public async Task<IActionResult> Details(int productId)
    {

        var product = await _productRepository.GetFirstOrDefaultAsync(p => p.Id == productId, including: nameof(Product.Category));

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

        var cartFromDb = await _shoppingCartRepository.GetFirstOrDefaultAsync(sc => sc.ApplicationUserId == userId &&
                                                                                sc.ProductId == shoppingCart.ProductId);

        if (cartFromDb is not null)
        {
            cartFromDb.Count += shoppingCart.Count;
            _shoppingCartRepository.Update(cartFromDb);
        }
        else
            _shoppingCartRepository.Add(shoppingCart);


        await _shoppingCartRepository.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
