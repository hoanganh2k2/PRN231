using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Net;

namespace eBookStore.Controllers
{
    public class BooksController : Controller
    {
        // GET: BooksController
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public BooksController(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7182/odata/")
            };
            _configuration = configuration;
        }
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Book?$expand=Publisher");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                await Console.Out.WriteLineAsync(response.RequestMessage.ToString());
                dynamic list = temp.value;
                IEnumerable<Book> books = ((JArray)temp.value).Select(x => new Book
                {
                    BookId = (int)x["BookId"],
                    Title = (string)x["Title"],
                    Advance = (string)x["Advance"],
                    Loyalty = (string)x["Loyalty"],
                    Notes = (string)x["Notes"],
                    Price = (int)x["Price"],
                    PublishDate = (DateTime)x["PublishDate"],
                    Publisher = new Publisher
                    {
                        PublisherId = (int)x["Publisher"]["PublisherId"],
                        PublisherName = (string)x["Publisher"]["PublisherName"],
                        // Add other properties of Publisher as needed
                    },
                    Sales = (int)x["Sales"],
                    Type = (string)x["Type"]
                });
                return View(books);
            }
            return View(null);
        }


        // GET: BooksController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Book?$expand=Publisher&filter=bookid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                Book books = new()
                {
                    BookId = (int)temp.value[0].BookId,
                    Title = (string)temp.value[0].Title,
                    Advance = (string)temp.value[0].Advance,
                    Loyalty = (string)temp.value[0].Loyalty,
                    Notes = (string)temp.value[0].Notes,
                    Price = (int)temp.value[0].Price,
                    PublishDate = (DateTime)temp.value[0].PublishDate,
                    Sales = (int)temp.value[0].Sales,
                    Type = (string)temp.value[0].Type,
                    Publisher = new Publisher
                    {
                        PublisherId = (int)temp.value[0].Publisher.PublisherId,
                        PublisherName = (string)temp.value[0].Publisher.PublisherName,
                    }
                };
                return View(books);
            }
            return NotFound();
        }

        // GET: BooksController/Create
        public async Task<ActionResult> Create()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Publisher");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Publisher> publishers = ((JArray)temp.value).Select(x => new Publisher
                {
                    PublisherId = (int)x["PublisherId"],
                    PublisherName = (string)x["PublisherName"],
                    City = (string)x["City"],
                    Country = (string)x["Country"],
                    State = (string)x["State"]
                });
                ViewData["PublisherId"] = new SelectList(publishers, "PublisherId", "PublisherName");
            }
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title,PublisherId,Type,Price,Advance,Loyalty,Sales,Notes,PublishDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                var bookData = new
                {
                    Title = book.Title,
                    PublisherId = book.PublisherId,
                    Type = book.Type,
                    Price = book.Price,
                    Advance = book.Advance,
                    Loyalty = book.Loyalty,
                    Sales = book.Sales,
                    Notes = book.Notes,
                    PublishDate = book.PublishDate.ToString("yyyy-MM-dd")
                };

                // Serialize the 'book' object to JSON
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Book", bookData);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to create the book.");
                }
            }
            HttpResponseMessage response1 = await _httpClient.GetAsync("Publisher");

            if (response1.IsSuccessStatusCode)
            {
                string content = await response1.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Publisher> publishers = ((JArray)temp.value).Select(x => new Publisher
                {
                    PublisherId = (int)x["PublisherId"],
                    PublisherName = (string)x["PublisherName"],
                    City = (string)x["City"],
                    Country = (string)x["Country"],
                    State = (string)x["State"]
                });
                ViewData["PublisherId"] = new SelectList(publishers, "PublisherId", "PublisherName");
            }

            return View(book);
        }

        // GET: BooksController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response1 = await _httpClient.GetAsync("Publisher");

            if (response1.IsSuccessStatusCode)
            {
                string content = await response1.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Publisher> publishers = ((JArray)temp.value).Select(x => new Publisher
                {
                    PublisherId = (int)x["PublisherId"],
                    PublisherName = (string)x["PublisherName"],
                    City = (string)x["City"],
                    Country = (string)x["Country"],
                    State = (string)x["State"]
                });
                ViewData["PublisherId"] = new SelectList(publishers, "PublisherId", "PublisherName");
            }
            HttpResponseMessage response = await _httpClient.GetAsync($"Book?$expand=Publisher&filter=bookid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                Book books = new()
                {
                    BookId = (int)temp.value[0].BookId,
                    Title = (string)temp.value[0].Title,
                    Advance = (string)temp.value[0].Advance,
                    Loyalty = (string)temp.value[0].Loyalty,
                    Notes = (string)temp.value[0].Notes,
                    Price = (int)temp.value[0].Price,
                    PublishDate = (DateTime)temp.value[0].PublishDate,
                    Sales = (int)temp.value[0].Sales,
                    Type = (string)temp.value[0].Type,
                    Publisher = new Publisher
                    {
                        PublisherId = (int)temp.value[0].Publisher.PublisherId,
                        PublisherName = (string)temp.value[0].Publisher.PublisherName,
                    },
                    PublisherId = (int)temp.value[0].PublisherId
                };


                return View(books);
            }

            return NotFound();
        }


        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("BookId,Title,PublisherId,Type,Price,Advance,Loyalty,Sales,Notes,PublishDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                var bookData = new
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PublisherId = book.PublisherId,
                    Type = book.Type,
                    Price = book.Price,
                    Advance = book.Advance,
                    Loyalty = book.Loyalty,
                    Sales = book.Sales,
                    Notes = book.Notes,
                    PublishDate = book.PublishDate.ToString("yyyy-MM-dd")
                };
                // Update the book by making a PUT request to the API
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Book", book);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update the book.");
                }
            }
            HttpResponseMessage response1 = await _httpClient.GetAsync("Publisher");

            if (response1.IsSuccessStatusCode)
            {
                string content = await response1.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Publisher> publishers = ((JArray)temp.value).Select(x => new Publisher
                {
                    PublisherId = (int)x["PublisherId"],
                    PublisherName = (string)x["PublisherName"],
                    City = (string)x["City"],
                    Country = (string)x["Country"],
                    State = (string)x["State"]
                });
                ViewData["PublisherId"] = new SelectList(publishers, "PublisherId", "PublisherName");
            }

            return View(book);
        }

        // GET: BooksController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Book?$expand=Publisher&filter=bookid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                Book books = new()
                {
                    BookId = (int)temp.value[0].BookId,
                    Title = (string)temp.value[0].Title,
                    Advance = (string)temp.value[0].Advance,
                    Loyalty = (string)temp.value[0].Loyalty,
                    Notes = (string)temp.value[0].Notes,
                    Price = (int)temp.value[0].Price,
                    PublishDate = (DateTime)temp.value[0].PublishDate,
                    Sales = (int)temp.value[0].Sales,
                    Type = (string)temp.value[0].Type,
                    Publisher = new Publisher
                    {
                        PublisherId = (int)temp.value[0].Publisher.PublisherId,
                        PublisherName = (string)temp.value[0].Publisher.PublisherName,
                    }
                };
                return View(books);
            }
            return NotFound();
        }

        // POST: BooksController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Book/" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the book.");
                return View();
            }
        }
        [HttpGet(Name = "Search")]
        public async Task<ActionResult> Filter(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction(nameof(Index));
            }

            HttpResponseMessage response;
            if (int.TryParse(keyword, out int result))
            {
                response = await _httpClient.GetAsync("Book?$expand=Publisher&filter=Price le " + keyword);
            }
            else
            {
                response = await _httpClient.GetAsync($"Book?$expand=Publisher&filter=Title eq '{keyword}'");
            }

            ViewBag.Keyword = keyword;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<Book> books = ((JArray)temp.value).Select(x => new Book
                {
                    BookId = (int)x["BookId"],
                    Title = (string)x["Title"],
                    Advance = (string)x["Advance"],
                    Loyalty = (string)x["Loyalty"],
                    Notes = (string)x["Notes"],
                    Price = (int)x["Price"],
                    PublishDate = (DateTime)x["PublishDate"],
                    Publisher = new Publisher
                    {
                        PublisherId = (int)x["Publisher"]["PublisherId"],
                        PublisherName = (string)x["Publisher"]["PublisherName"],
                    },
                    Sales = (int)x["Sales"],
                    Type = (string)x["Type"]
                });
                return View("Index", books);
            }

            return View("Index", null);
        }

    }
}
