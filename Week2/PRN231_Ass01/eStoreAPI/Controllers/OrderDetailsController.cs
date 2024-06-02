using BusinessObject.Models; // Ensure you have the correct namespace for OrderDetail
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository = new OrderDetailRepository();


        [HttpGet]
        public async Task<IEnumerable<OrderDetail>> GetOrdersDetail()
        {
            IEnumerable<OrderDetail> orderDetails = await _orderDetailRepository.GetAllOrderDetailsAsync();
            return orderDetails;
        }


        [HttpGet("{id:int}", Name = "GetOrderDetail")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            OrderDetail orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return orderDetail;
        }


        [HttpPost]
        public async Task<IActionResult> Post(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return BadRequest();
            }

            await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(OrderDetail orderDetail)
        {
            await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            OrderDetail orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            await _orderDetailRepository.DeleteOrderDetailAsync(id);

            return NoContent();
        }
    }
}
