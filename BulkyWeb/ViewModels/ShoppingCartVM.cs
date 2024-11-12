using Domain.Models;

namespace BulkyWeb.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<ShoppingCart> CartList { get; set; }
    public double OrderTotal { get; set; }
}
