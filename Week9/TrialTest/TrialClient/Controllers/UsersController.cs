using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace TrialClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsersController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7025/api/")
            };
        }
        // GET: Members
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("user");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
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

            HttpResponseMessage response = await _httpClient.GetAsync("user?$filter=userid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
                return View(users.First());
            }
            return View(null);
        }

        // GET: Members/Create
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage addressResponse = await _httpClient.GetAsync("address");
            if (!addressResponse.IsSuccessStatusCode) return View();

            string addressContent = await addressResponse.Content.ReadAsStringAsync();
            List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(addressContent);

            ViewBag.AddressId = new SelectList(addresses, "AddressId", "AddressName");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User member)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("user", member);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction("Index", "Users");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This User is already existed!");
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            HttpResponseMessage response = await _httpClient.GetAsync("user?$filter=userid eq " + id);

            if (!response.IsSuccessStatusCode) return View(null);

            string content = await response.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
            User user = users.FirstOrDefault();

            if (user == null) return NotFound();

            HttpResponseMessage addressResponse = await _httpClient.GetAsync("address");
            if (!addressResponse.IsSuccessStatusCode) return View(user);

            string addressContent = await addressResponse.Content.ReadAsStringAsync();
            List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(addressContent);

            ViewBag.AddressId = new SelectList(addresses, "AddressId", "AddressName", user.AddressId);

            return View(user);
        }


        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User member)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync("user", member);
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index", "Users");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This User is already existed!");
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

            HttpResponseMessage response = await _httpClient.GetAsync("user?$filter=userid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
                return View(users.First());
            }
            return View(null);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"user/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
