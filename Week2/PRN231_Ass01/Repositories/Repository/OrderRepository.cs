using BusinessObject;
using BusinessObject.Models;
using DataAccess;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private OrderDAO _context { get; }

        public OrderRepository() => _context = new OrderDAO(new MyDbContext());
        public async Task<int> AddOrderAsync(Order order) => await _context.AddOrderAsync(order);


        public async Task DeleteOrderAsync(int orderId) => await _context.DeleteOrderAsync(orderId);

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() => await _context.GetAllOrdersAsync();

        public async Task<Order> GetOrderByIdAsync(int orderId) => await _context.GetOrderByIdAsync(orderId);

        public async Task<IEnumerable<Order>> GetOrdersByMemberEmailAsync(string email) => await _context.GetOrdersByMemberEmailAsync(email);

        public async Task UpdateOrderAsync(Order order) => await _context.UpdateOrderAsync(order);
    }
}
