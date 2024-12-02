using DataAccess.Data;
using DataAccess.Repository.ShoppingCarts;
using Domain.Enums;
using Domain.Models;
using System.Security.Claims;

namespace Service.ShoppingCarts;
public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ApplicationDbContext _context;

    public ShoppingCartService(
        IShoppingCartRepository shoppingCartRepository,
        ApplicationDbContext context)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _context = context;
    }

    public Task<ShoppingCart?> GetShoppingCartForUserAsync(string userId, int productId) =>
        _shoppingCartRepository.GetFirstOrDefaultAsync(
            sc => sc.ApplicationUserId == userId &&
            sc.ProductId == productId);


    public Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsForUserAsync(string userId) =>
    _shoppingCartRepository.GetAllAsync(
        sc => sc.ApplicationUserId == userId,
        including: nameof(ShoppingCart.Product));


    public async Task UpdateShoppingCart(ShoppingCart cartFromDb, ShoppingCart shoppingCart)
    {
        cartFromDb.Count += shoppingCart.Count;
        _shoppingCartRepository.Update(cartFromDb);
        await _shoppingCartRepository.SaveChangesAsync();
    }


    public async Task CreateNewShoppingCart(ShoppingCart cart)
    {
        _shoppingCartRepository.Add(cart);
        await _shoppingCartRepository.SaveChangesAsync();
    }


    public double CalculateTotalOrderPrice(IEnumerable<ShoppingCart> cartList)
    {
        return cartList.Sum(cart =>
        {
            cart.Price = GetPricesBasedOnQuantity(cart);
            return cart.Price * cart.Count;
        });

        static double GetPricesBasedOnQuantity(ShoppingCart shoppingCart) =>
            shoppingCart.Count switch
            {
                <= 50 => shoppingCart.Product.Price,
                <= 100 => shoppingCart.Product.PriceFor50,
                _ => shoppingCart.Product.PriceFor100
            };
    }


    public async Task IncrementProductCountAsync(int cartId)
    {
        var cart = await _shoppingCartRepository.GetFirstOrDefaultAsync(sc => sc.Id == cartId);
        ArgumentNullException.ThrowIfNull(cart, nameof(cart));

        cart.Count += 1;
        _shoppingCartRepository.Update(cart);
        await _shoppingCartRepository.SaveChangesAsync();
    }



    public async Task DecrementProductCount(int cartId)
    {
        var cart = await _shoppingCartRepository.GetFirstOrDefaultAsync(sc => sc.Id == cartId);
        ArgumentNullException.ThrowIfNull(cart, nameof(cart));

        if (cart.Count <= 1)
        {
            _shoppingCartRepository.Remove(cart);
        }
        else
        {
            cart.Count -= 1;
            _shoppingCartRepository.Update(cart);
        }

        await _shoppingCartRepository.SaveChangesAsync();
    }


    public async Task RemoveShoppingCartAsync(int cartId)
    {
        var cart = await _shoppingCartRepository.GetFirstOrDefaultAsync(sc => sc.Id == cartId);
        ArgumentNullException.ThrowIfNull(cart, nameof(cart));

        _shoppingCartRepository.Remove(cart);
        await _shoppingCartRepository.SaveChangesAsync();
    }


    public int GetCartTotalItemCount(ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null) return 0;

        return _shoppingCartRepository.GetCartTotalItemCount(userId);
    }

    public async Task CreateOrderHeader(OrderHeader orderHeader, ApplicationUser user, double totalPrice)
    {
        orderHeader.ApplicationUser = user;
        orderHeader.OrderTotal = totalPrice;
        if (IsUserCompanyUser(user))
        {
            orderHeader.PaymentStatus = nameof(PaymentStatus.DelayedPayment);
            orderHeader.OrderStatus = nameof(OrderStatus.Approved);
        }
        else
        {
            orderHeader.PaymentStatus = nameof(PaymentStatus.Pending);
            orderHeader.OrderStatus = nameof(OrderStatus.Pending);
        }

        _context.OrderHeaders.Add(orderHeader);
        await _context.SaveChangesAsync();

        static bool IsUserCompanyUser(ApplicationUser user) => user.CompanyId.GetValueOrDefault() == 0;
    }

    public async Task CreateOrderDetails(IEnumerable<ShoppingCart> cartList, OrderHeader orderHeader)
    {
        List<Task> orderDetailsTasks = [];
        foreach (var cart in cartList)
        {
            var orderDetail = new OrderDetail
            {
                ProductId = cart.ProductId,
                OrderHeaderId = orderHeader.Id,
                Price = cart.Price,
                Count = cart.Count
            };
            _context.OrderDetails.Add(orderDetail);
            orderDetailsTasks.Add(_context.SaveChangesAsync());
        };

        await Task.WhenAll(orderDetailsTasks);
    }
}
