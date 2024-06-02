using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eStoreAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository = new OrderRepository();

        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }


        [HttpGet("{id:int}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (id <= 0)
                return BadRequest();

            Order order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                if (order == null)
                    return NotFound();

            return Ok(order);
        }


        [HttpPost]
        public async Task<ActionResult<int>> PostOrder(Order order)
        {
            int id = await _orderRepository.AddOrderAsync(order);
            return Ok(id);
        }


        [HttpPut("{id:int}", Name = "PutOrder")]
        public void PutOrder(int id, [FromBody] string value)
        {
            //not need to use
        }


        [HttpDelete("{id:int}", Name = "DeleteOrder")]
        public void DeleteOrder(int id)
        {
            //not need to use
        }

        [HttpGet("member")]
        public async Task<IEnumerable<Order>> GetOrderByEmail(string email)
        {
            return await _orderRepository.GetOrdersByMemberEmailAsync(email);
        }
    }
}
