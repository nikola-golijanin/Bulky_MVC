using DataAccess.Repository.ShoppingCarts;
using Domain.Models;

namespace Service.ShoppingCarts;
public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    public async Task CreateNewShoppingCart(ShoppingCart cart)
    {
        _shoppingCartRepository.Add(cart);
        await _shoppingCartRepository.SaveChangesAsync();
    }

    public Task<ShoppingCart?> GetShoppingCartForUserAsync(string userId, int productId) =>
        _shoppingCartRepository.GetFirstOrDefaultAsync(
            sc => sc.ApplicationUserId == userId &&
            sc.ProductId == productId);

    public async Task UpdateShoppingCart(ShoppingCart cartFromDb, ShoppingCart shoppingCart)
    {
        cartFromDb.Count += shoppingCart.Count;
        _shoppingCartRepository.Update(cartFromDb);
        await _shoppingCartRepository.SaveChangesAsync();
    }
}
