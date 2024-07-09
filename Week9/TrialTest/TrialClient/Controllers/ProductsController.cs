using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace TrialClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductsController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7025/api/")
            };
        }
        // GET: Members
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("product");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(content);
                return View(products);
            }
            return View(null);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync("product?$filter=productid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(content);
                return View(products.First());
            }
            return View(null);
        }

        // GET: Members/Create
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage userResponse = await _httpClient.GetAsync("user");
            if (!userResponse.IsSuccessStatusCode) return View();

            string userContent = await userResponse.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(userContent);

            ViewBag.UserId = new SelectList(users, "UserId", "Name");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("product", product);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction("Index", "Products");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This Product is already existed!");
            }
            return View(product);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            HttpResponseMessage response = await _httpClient.GetAsync("product?$filter=productid eq " + id);

            if (!response.IsSuccessStatusCode) return View(null);

            string content = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(content);
            Product product = products.FirstOrDefault();

            if (product == null) return NotFound();

            HttpResponseMessage addressResponse = await _httpClient.GetAsync("user");
            if (!addressResponse.IsSuccessStatusCode) return View(product);

            string userContent = await addressResponse.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(userContent);

            ViewBag.UserId = new SelectList(users, "UserId", "Name", product.UserId);

            return View(product);
        }


        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync("product", product);
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index", "Products");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This Product is already existed!");
            }
            return View(product);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync("product?$filter=productid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(content);
                return View(products.First());
            }
            return View(null);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"product/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
