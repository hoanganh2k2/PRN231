using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eStoreClient2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CartsController(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7254/api/")
            };
            _configuration = configuration;
        }
        [HttpGet("Index/{id?}", Name = "CartIndex")]
        public async Task<IActionResult> Index(int? id)
        {
            string orderId = id.ToString();
            if (id == null)
                orderId = HttpContext.Session.GetString("OrderId");
            Order? order = await _httpClient.GetFromJsonAsync<Order>($"orders/" + orderId);
            return View(order);
        }
        [HttpGet("MyOrders", Name = "MyOrders")]
        public async Task<IActionResult> MyOrders()
        {
            string isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != null)
            {
                IEnumerable<Order>? orders = await _httpClient.GetFromJsonAsync<IEnumerable<Order>>("orders");
                return View(orders);
            }
            string member = HttpContext.Session.GetString("CurrentUser");
            if (member == null)
                return RedirectToAction("Home", "Index");
            IEnumerable<Order>? order = await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"orders/member?email=" + member);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            HttpContext.Session.Remove("OrderId");
            return RedirectToAction("Index", "Home");
        }
    }
}
