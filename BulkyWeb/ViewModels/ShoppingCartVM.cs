using Domain.Models;

namespace BulkyWeb.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<ShoppingCart> ListCart { get; set; }
    public double OrderTotal { get; set; }
}
