using BusinessObject.Model;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repository = new OrderRepository();
        private readonly IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        private readonly IProductRepository productRepository = new ProductRepository();

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders() => Ok(repository.GetOrders());

        //[Authorize(Roles = UserRoles.Customer)]
        [HttpGet("customer/{id}")]
        public ActionResult<IEnumerable<Order>> GetAllOrdersByCustomerId(string id)
        {
            List<Order> listOrder = repository.GetAllOrdersByCustomerId(id);
            foreach (Order o in listOrder)
            {
                List<OrderDetail> orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(o.OrderID);
                o.OrderDetails = orderDetails;
            }
            return Ok(listOrder);
        }

        //[Authorize(Roles = UserRoles.Customer)]
        [HttpGet("customer/detail/{id}")]
        public ActionResult<Order> GetOrderDetailById(int id)
        {
            Order order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            List<OrderDetail> orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(id);
            order.OrderDetails = orderDetails;
            return Ok(order);
        }


        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            Order order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            List<OrderDetail> orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(id);
            order.OrderDetails = orderDetails;
            return Ok(order);
        }

        //[Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public ActionResult<Order> PostOrder(PostOrder postOrder)
        {
            foreach (PostOrderDetail od in postOrder.OrderDetails)
            {
                Product fb = productRepository.GetProductById(od.ProductID);
                if (fb == null)
                {
                    return NotFound();
                }
                if (fb.UnitsInStock < od.Quantity)
                {
                    return BadRequest();
                }
            }
            Order order = new()
            {
                OrderDate = postOrder.OrderDate,
                ShippedDate = null,
                Total = postOrder.Total,
                Freight = postOrder.Freight,
                CustomerID = postOrder.CustomerID
            };
            Order savedOrder = repository.SaveOrder(order);
            foreach (PostOrderDetail od in postOrder.OrderDetails)
            {
                Product fb = productRepository.GetProductById(od.ProductID);
                fb.UnitsInStock -= od.Quantity;
                OrderDetail orderDetail = new()
                {
                    ProductID = od.ProductID,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    OrderID = savedOrder.OrderID,
                    Discount = 0
                };
                productRepository.UpdateProduct(fb);
                orderDetailRepository.SaveOrderDetail(orderDetail);
            }
            return Ok(savedOrder);
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut("shipped/{id}")]
        public IActionResult PutOrderShipped(int id)
        {
            Order oTmp = repository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }
            oTmp.ShippedDate = DateTime.Now;
            repository.UpdateOrder(oTmp);
            return NoContent();
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut("cancel/{id}")]
        public IActionResult PutOrderCancel(int id)
        {
            Order oTmp = repository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }
            repository.UpdateOrder(oTmp);
            List<OrderDetail> orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(id);
            foreach (OrderDetail od in orderDetails)
            {
                Product fb = productRepository.GetProductById(od.ProductID);
                fb.UnitsInStock += od.Quantity;
                productRepository.UpdateProduct(fb);
            }
            return NoContent();
        }
    }
}
