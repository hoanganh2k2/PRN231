using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace eBookStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsersController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7182/odata/")
            };
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("User");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<User> users = ((JArray)temp.value).Select(x => new User
                {
                    Email = (string)x["Email"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                    MiddleName = (string)x["MiddleName"],
                    Password = (string)x["Password"],
                    Source = (string)x["Source"],
                    UserId = (int)x["UserId"]
                });
                return View(users);
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

            HttpResponseMessage response = await _httpClient.GetAsync("User?filter=userid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<User> users = ((JArray)temp.value).Select(x => new User
                {
                    Email = (string)x["Email"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                    MiddleName = (string)x["MiddleName"],
                    Password = (string)x["Password"],
                    Source = (string)x["Source"],
                    UserId = (int)x["UserId"]
                });
                return View(users.First());
            }
            return View(null);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Email,Password,FirstName,MiddleName,LastName,Source")] User member)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("User", member);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    HttpContext.Session.SetString("CurrentUser", member.Email);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This Email is already existed!");
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync("User?filter=userid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<User> users = ((JArray)temp.value).Select(x => new User
                {
                    Email = (string)x["Email"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                    MiddleName = (string)x["MiddleName"],
                    Password = (string)x["Password"],
                    Source = (string)x["Source"],
                    UserId = (int)x["UserId"]
                });
                return View(users.First());
            }
            return View(null);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Email,Password,FirstName,MiddleName,LastName,Source")] User member)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync("User", member);
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return Redirect("Index");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This Email is already existed!");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync("User?filter=userid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic temp = JObject.Parse(content);
                dynamic list = temp.value;
                IEnumerable<User> users = ((JArray)temp.value).Select(x => new User
                {
                    Email = (string)x["Email"],
                    FirstName = (string)x["FirstName"],
                    LastName = (string)x["LastName"],
                    MiddleName = (string)x["MiddleName"],
                    Password = (string)x["Password"],
                    Source = (string)x["Source"],
                    UserId = (int)x["UserId"]
                });
                return View(users.First());
            }
            return View(null);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"User/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
