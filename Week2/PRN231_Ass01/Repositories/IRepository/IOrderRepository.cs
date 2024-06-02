using BusinessObject.Models;

namespace Repositories.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<int> AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByMemberEmailAsync(string email);
    }

}
