using Domain.Models;

namespace DataAccess.Repository.Orders;
public interface IOrderHeaderRepository : IRepository<OrderHeader>
{
    public void Update(OrderHeader orderHeader);
}
