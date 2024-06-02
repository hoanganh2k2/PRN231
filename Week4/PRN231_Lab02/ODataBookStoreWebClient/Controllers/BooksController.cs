using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Net.Http.Headers;

namespace ODataBookStoreWebClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient client = null;
        private readonly string BookApiUrl = "";
        public BooksController()
        {
            client = new HttpClient();
            MediaTypeWithQualityHeaderValue contentType = new("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:7201/odata/Books/";
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(BookApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            dynamic lst = temp.value;
            List<Book> items = ((JArray)temp.value).Select(x => new Book
            {
                Id = (int)x["Id"],
                Author = (string)x["Author"],
                ISBN = (string)x["ISBN"],
                Title = (string)x["Title"],
                Price = (decimal)x["Price"],
            }).ToList();

            return View(items);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            string requestUri = $"{BookApiUrl}{id}";
            Book? response = await client.GetFromJsonAsync<Book>(requestUri);

            if (response == null) return NotFound();

            return View(response);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.Location == null)
                {
                    book.Location = new Address();
                }

                if (book.Press == null)
                {
                    book.Press = new Press();
                }

                HttpResponseMessage response = await client.PostAsJsonAsync(BookApiUrl, book);

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            string requestUri = $"{BookApiUrl}{id}";
            Book? response = await client.GetFromJsonAsync<Book>(requestUri);

            if (response == null) return NotFound();

            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();

            //if (ModelState.IsValid)
            //{
            HttpResponseMessage response = await client.PutAsJsonAsync($"{BookApiUrl}{id}", book);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
            //}

            //return View(book);
        }

        // GET: Books/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            string requestUri = $"{BookApiUrl}{id}";
            Book? response = await client.GetFromJsonAsync<Book>(requestUri);

            if (response == null) return NotFound();

            return View(response);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{BookApiUrl}{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
