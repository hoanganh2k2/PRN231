using BusinessObject.Model;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> listOrderDetails = new();
            try
            {
                using (MyDBContext context = new())
                {
                    listOrderDetails = context.OrderDetails.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrderDetails;
        }

        public static List<OrderDetail> FindAllOrderDetailsByProductId(int productId)
        {
            List<OrderDetail> listOrderDetails = new();
            try
            {
                using (MyDBContext context = new())
                {
                    listOrderDetails = context
                        .OrderDetails
                        .Where(o => o.ProductID == productId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetails;
        }

        public static List<OrderDetail> FindAllOrderDetailsByOrderId(int orderId)
        {
            List<OrderDetail> listOrderDetails = new();
            try
            {
                using (MyDBContext context = new())
                {
                    listOrderDetails = context
                        .OrderDetails
                        .Where(o => o.OrderID == orderId)
                        .ToList();
                    listOrderDetails.ForEach(o =>
                        o.Product = context.Products.SingleOrDefault(f => f.ProductID == o.ProductID)
                    );
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetails;
        }

        public static OrderDetail FindOrderDetailByOrderIdAndProductId(int orderId, int productId)
        {
            OrderDetail orderDetail = new();
            try
            {
                using (MyDBContext context = new())
                {
                    orderDetail = context
                        .OrderDetails
                        .SingleOrDefault(o => o.OrderID == orderId && o.ProductID == productId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public static void SaveOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    context.Entry(orderDetail).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    OrderDetail? orderDetailToDelete = context
                        .OrderDetails
                        .SingleOrDefault(o => o.OrderID == orderDetail.OrderID && o.ProductID == orderDetail.ProductID);
                    context.OrderDetails.Remove(orderDetailToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
