using BusinessObject;
using BusinessObject.Models;
using DataAccess;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly OrderDetailDAO _context;
        public OrderDetailRepository() => _context = new OrderDetailDAO(new MyDbContext());
        public async Task AddOrderDetailAsync(OrderDetail orderDetail) => await _context.AddOrderDetailAsync(orderDetail);

        public async Task DeleteOrderDetailAsync(int orderDetailId) => await _context.DeleteOrderDetailAsync(orderDetailId);


        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync() => await _context.GetAllOrderDetailsAsync();

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId) => await _context.GetOrderDetailByIdAsync(orderDetailId);

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail) => await _context.UpdateOrderDetailAsync(orderDetail);

    }
}
