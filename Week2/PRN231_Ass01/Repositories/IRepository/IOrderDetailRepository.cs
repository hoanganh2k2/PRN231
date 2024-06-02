using BusinessObject.Models;

namespace Repositories.IRepository
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId);
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task DeleteOrderDetailAsync(int orderDetailId);
    }

}
