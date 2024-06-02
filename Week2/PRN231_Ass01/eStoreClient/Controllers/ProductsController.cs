using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStoreClient2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private ICollection<Product> _products;
        public ProductsController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7254/api/")
            };
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product>? response = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("products");
            _products = response.ToList();
            return View(response);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product? response = await _httpClient.GetFromJsonAsync<Product>($"products/{id}");
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Category>? response = await _httpClient.GetFromJsonAsync<IEnumerable<Category>>("products/Category");
            ViewData["CategoryId"] = new SelectList(response, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("products", product);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<Category>? responses = await _httpClient.GetFromJsonAsync<IEnumerable<Category>>("Products/Category");
            ViewData["CategoryId"] = new SelectList(responses, "CategoryId", "CategoryName");
            Product? response = await _httpClient.GetFromJsonAsync<Product>($"products/{id}");
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"products/{id}", product);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product? response = await _httpClient.GetFromJsonAsync<Product>($"products/{id}");
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"products/{id}");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddToCart(int id, int price)
        {
            string user = HttpContext.Session.GetString("CurrentUser");
            Task<Member?> member = _httpClient.GetFromJsonAsync<Member>($"members/" + user);
            string orderId = HttpContext.Session.GetString("OrderId");
            if (orderId == null)
            {
                Order order = new()
                {
                    MemberId = member.Id,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now,
                    ShippedDate = DateTime.Now.AddDays(3),
                    Freight = 0
                };
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("orders", order);
                orderId = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("OrderId", orderId);
            }
            await Console.Out.WriteLineAsync(orderId);
            OrderDetail orderDetail = new()
            {
                OrderId = int.Parse(orderId),
                Discount = 0,
                ProductId = id,
                Quantity = 1,
                UnitPrice = price
            };
            await _httpClient.PostAsJsonAsync("OrderDetails", orderDetail);
            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                IEnumerable<Product>? response1 = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("products");
                _products = response1.ToList();
                return View("Index", response1);
            }
            IEnumerable<Product>? response = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("products/search?keyword=" + keyword);

            ViewBag.Keyword = keyword;

            return View("Index", response);
        }

    }
}
