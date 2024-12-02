using Domain.Models;
using System.Security.Claims;

namespace Service.ShoppingCarts;
public interface IShoppingCartService
{
    Task<ShoppingCart?> GetShoppingCartForUserAsync(string userId, int productId);

    Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsForUserAsync(string userId);

    Task UpdateShoppingCart(ShoppingCart cartFromDb, ShoppingCart shoppingCart);

    Task CreateNewShoppingCart(ShoppingCart cart);

    double CalculateTotalOrderPrice(IEnumerable<ShoppingCart> cartList);

    Task IncrementProductCountAsync(int cartId);

    Task DecrementProductCount(int cartId);

    Task RemoveShoppingCartAsync(int cartId);

    int GetCartTotalItemCount(ClaimsPrincipal user);

    Task CreateOrderHeader(OrderHeader orderHeader, ApplicationUser user, double totalPrice);

    Task CreateOrderDetails(IEnumerable<ShoppingCart> cartList, OrderHeader orderHeader);
}
