using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductManagementWebClient.Views.Products
{
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        private readonly string ProductApiUrl = "";
        public ProductsController()
        {
            client = new HttpClient();
            MediaTypeWithQualityHeaderValue contenType = new("application/json");
            client.DefaultRequestHeaders.Accept.Add(contenType);
            ProductApiUrl = "https://localhost:7044/api/products";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            using JsonDocument document = JsonDocument.Parse(strData);
            JsonElement valuesArray = document.RootElement.GetProperty("$values");

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(valuesArray.GetRawText(), options);

            return View(listProducts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            string url = $"{ProductApiUrl}/{id}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return NotFound();

                string strData = await response.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                Product product = JsonSerializer.Deserialize<Product>(strData, options);

                if (product == null)
                    return NotFound();

                return View(product);
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving product data");
            }
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                List<Category> listPCategories = await GetCategoriesAsync();
                ViewData["CategoryId"] = new SelectList(listPCategories, "CategoryId", "CategoryName");
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving categories data");
            }
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
        {
            product.Category = new Category { CategoryId = 0, CategoryName = "string", Products = new List<Product>() };

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            StringContent content = new(JsonSerializer.Serialize(product, options), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(ProductApiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "There was an error saving the product.");
            }

            string url = "https://localhost:7044/api/categories";
            try
            {
                List<Category> listPCategories = await GetCategoriesAsync();
                ViewData["CategoryId"] = new SelectList(listPCategories, "CategoryId", "CategoryName");
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving categories data");
            }
            return View(product);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            string url = "https://localhost:7044/api/categories";
            HttpResponseMessage response = await client.GetAsync(url);

            string strData = await response.Content.ReadAsStringAsync();

            using JsonDocument document = JsonDocument.Parse(strData);
            JsonElement valuesArray = document.RootElement.GetProperty("$values");

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Category>>(valuesArray.GetRawText(), options);
        }

        // GET: Products/Edit/5
        /*        public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null || _context.Products == null)
                    {
                        return NotFound();
                    }

                    Product? product = await _context.Products.FindAsync(id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
                    return View(product);
                }

                // POST: Products/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
                {
                    if (id != product.ProductId)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(product);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ProductExists(product.ProductId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
                    return View(product);
                }*/

        /*// GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            Product? product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MyDbContext.Products'  is null.");
            }
            Product? product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }*/
    }
}
