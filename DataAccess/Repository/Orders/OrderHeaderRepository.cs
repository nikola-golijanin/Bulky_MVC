using DataAccess.Data;
using Domain.Models;

namespace DataAccess.Repository.Orders;
public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private readonly ApplicationDbContext _context;
    public OrderHeaderRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(OrderHeader orderHeader)
    {
        _context.Update(orderHeader);
    }
}
