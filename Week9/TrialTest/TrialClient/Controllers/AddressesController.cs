using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace TrialClient.Controllers
{
    public class AddressesController : Controller
    {
        private readonly HttpClient _httpClient;

        public AddressesController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7025/api/")
            };
        }
        // GET: Members
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("address");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(content);
                return View(addresses);
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

            HttpResponseMessage response = await _httpClient.GetAsync("address?$filter=addressid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(content);
                return View(addresses.First());
            }
            return View(null);
        }

        // GET: Members/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("address", address);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction("Index", "Addresses");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This Address is already existed!");
            }
            return View(address);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            HttpResponseMessage response = await _httpClient.GetAsync("address?$filter=addressid eq " + id);

            if (!response.IsSuccessStatusCode) return View(null);

            string content = await response.Content.ReadAsStringAsync();
            List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(content);
            Address address = addresses.FirstOrDefault();

            if (address == null) return NotFound();
            return View(address);
        }


        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Address address)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync("address", address);
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index", "Addresses");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This Address is already existed!");
            }
            return View(address);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync("address?$filter=addressid eq " + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(content);
                return View(addresses.First());
            }
            return View(null);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"address/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
