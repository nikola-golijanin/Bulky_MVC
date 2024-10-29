using Domain.Models;

namespace DataAccess.Repository.ShoppingCarts;
public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    void Update(ShoppingCart cart);
}