using Domain.Models;

namespace DataAccess.Repository.Orders;
public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    public void Update(OrderDetail orderDetail);
}
