using Domain.Models;

namespace Service.ShoppingCarts;
public interface IShoppingCartService
{
    Task<ShoppingCart?> GetShoppingCartForUserAsync(string userId, int productId);

    Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsForUser(string userId);

    Task UpdateShoppingCart(ShoppingCart cartFromDb, ShoppingCart shoppingCart);

    Task CreateNewShoppingCart(ShoppingCart cart);
}
