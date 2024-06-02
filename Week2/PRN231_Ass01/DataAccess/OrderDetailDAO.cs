using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private readonly MyDbContext _dbContext;

        public OrderDetailDAO(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _dbContext.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId)
        {
            return await _dbContext.OrderDetails.FindAsync(orderDetailId);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            OrderDetail? existingOrderDetail = await _dbContext.OrderDetails
                .FirstOrDefaultAsync(od => od.ProductId == orderDetail.ProductId && od.OrderId == orderDetail.OrderId);

            if (existingOrderDetail != null)
            {
                existingOrderDetail.Quantity += orderDetail.Quantity;
            }
            else
            {
                _dbContext.OrderDetails.Add(orderDetail);
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _dbContext.Entry(orderDetail).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int orderDetailId)
        {
            OrderDetail? orderDetail = await _dbContext.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail != null)
            {
                _dbContext.OrderDetails.Remove(orderDetail);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
