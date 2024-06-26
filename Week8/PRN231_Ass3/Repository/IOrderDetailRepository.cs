using BusinessObject.Model;

namespace Repositories
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail orderDetail);
        OrderDetail GetOrderDetailByOrderIdAndProductId(int orderId, int productId);
        List<OrderDetail> GetOrderDetails();
        List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail orderDetail);
    }
}
