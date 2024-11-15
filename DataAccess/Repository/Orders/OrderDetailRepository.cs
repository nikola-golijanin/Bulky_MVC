using DataAccess.Data;
using Domain.Models;

namespace DataAccess.Repository.Orders;
public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _context;
    public OrderDetailRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(OrderDetail orderDetail)
    {
        _context.Update(orderDetail);
    }
}
