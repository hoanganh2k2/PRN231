using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace eBookStore.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: AuthorsController
        private readonly HttpClient _httpClient;

        public AuthorsController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7182/odata/")
            };
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Author");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Author> authors = ((JArray)temp.value).Select(x => new Author
                {
                    AuthorId = (int)x["AuthorId"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                    Phone = (string)x["Phone"],
                    Address = (string)x["Address"],
                    City = (string)x["City"],
                    State = (string)x["State"],
                    Zip = (string)x["Zip"],
                    Email = (string)x["Email"]
                });
                return View(authors);
            }
            return View(null);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync("Author?filter=AuthorId eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Author> authors = ((JArray)temp.value).Select(x => new Author
                {
                    AuthorId = (int)x["AuthorId"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                    Phone = (string)x["Phone"],
                    Address = (string)x["Address"],
                    City = (string)x["City"],
                    State = (string)x["State"],
                    Zip = (string)x["Zip"],
                    Email = (string)x["Email"]
                });
                return View(authors.First());
            }
            return View(null);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,FirstName,LastName,Phone,Address,City,State,Zip,Email")] Author author)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Author", author);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Failed to create the author.");
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync("Author?filter=AuthorId eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Author> authors = ((JArray)temp.value).Select(x => new Author
                {
                    AuthorId = (int)x["AuthorId"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                    Phone = (string)x["Phone"],
                    Address = (string)x["Address"],
                    City = (string)x["City"],
                    State = (string)x["State"],
                    Zip = (string)x["Zip"],
                    Email = (string)x["Email"]
                });
                return View(authors.First());
            }
            return View(null);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,FirstName,LastName,Phone,Address,City,State,Zip,Email")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync("Author", author);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Failed to update the author.");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync("Author?filter=AuthorId eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Author> authors = ((JArray)temp.value).Select(x => new Author
                {
                    AuthorId = (int)x["AuthorId"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                    Phone = (string)x["Phone"],
                    Address = (string)x["Address"],
                    City = (string)x["City"],
                    State = (string)x["State"],
                    Zip = (string)x["Zip"],
                    Email = (string)x["Email"]
                });
                return View(authors.First());
            }
            return View(null);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Author/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
