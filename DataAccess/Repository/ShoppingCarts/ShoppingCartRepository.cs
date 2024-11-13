using DataAccess.Data;
using Domain.Models;

namespace DataAccess.Repository.ShoppingCarts;
public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly ApplicationDbContext _context;
    public ShoppingCartRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public int GetCartTotalItemCount(string userId) =>
        _context.ShoppingCarts
            .Where(cart => cart.ApplicationUserId == userId)
            .Select(sc => sc.Count)
            .Sum();

    public void Update(ShoppingCart cart) => _context.ShoppingCarts.Update(cart);
}
