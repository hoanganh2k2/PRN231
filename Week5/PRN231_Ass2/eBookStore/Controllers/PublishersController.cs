using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace eBookStore.Controllers
{
    public class PublishersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PublishersController(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7182/odata/")
            };
            _configuration = configuration;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
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
                return View(publishers);
            }
            return View(null);
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"Publisher?filter=publisherid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                Publisher publisher = new()
                {
                    PublisherId = (int)temp.value[0].PublisherId,
                    PublisherName = (string)temp.value[0].PublisherName,
                    City = (string)temp.value[0].City,
                    Country = (string)temp.value[0].Country,
                    State = (string)temp.value[0].State
                };
                return View(publisher);
            }
            return NotFound();
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherId,PublisherName,City,Country,State")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Publisher", publisher);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Failed to create the publisher.");
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"Publisher?filter=publisherid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                Publisher publisher = new()
                {
                    PublisherId = (int)temp.value[0].PublisherId,
                    PublisherName = (string)temp.value[0].PublisherName,
                    City = (string)temp.value[0].City,
                    Country = (string)temp.value[0].Country,
                    State = (string)temp.value[0].State
                };
                return View(publisher);
            }
            return NotFound();
        }

        // POST: Publishers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,PublisherName,City,Country,State")] Publisher publisher)
        {
            if (id != publisher.PublisherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Publisher", publisher);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Failed to update the publisher.");
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"Publisher?filter=publisherid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                Publisher publisher = new()
                {
                    PublisherId = (int)temp.value[0].PublisherId,
                    PublisherName = (string)temp.value[0].PublisherName,
                    City = (string)temp.value[0].City,
                    Country = (string)temp.value[0].Country,
                    State = (string)temp.value[0].State
                };
                return View(publisher);
            }
            return NotFound();
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Publisher");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
        }
    }
}
