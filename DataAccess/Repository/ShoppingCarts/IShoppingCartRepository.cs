using Domain.Models;

namespace DataAccess.Repository.ShoppingCarts;
public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    int GetCartTotalItemCount(string userId);

    void Update(ShoppingCart cart);
}