using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderDAO
    {
        private readonly MyDbContext _dbContext;

        public OrderDAO(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders.Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<int> AddOrderAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            Order? order = await _dbContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByMemberEmailAsync(string email)
        {
            return await _dbContext.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.Member != null && o.Member.Email == email)
                .ToListAsync();
        }
    }
}
