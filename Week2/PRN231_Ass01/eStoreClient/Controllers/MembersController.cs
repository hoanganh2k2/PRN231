using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eStoreClient2.Controllers
{
    public class MembersController : Controller
    {
        private readonly HttpClient _httpClient;

        public MembersController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7254/api/")
            };
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            IEnumerable<Member>? response = await _httpClient.GetFromJsonAsync<IEnumerable<Member>>("members");
            return View(response);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member? response = await _httpClient.GetFromJsonAsync<Member>($"members/{id}");
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
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
        public async Task<IActionResult> Create([Bind("MemberId,Email,Password,CompanyName,City,Country")] Member member)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("members", member);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
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
            Member? response = await _httpClient.GetFromJsonAsync<Member>($"members/{id}");
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Email,Password,CompanyName,City,Country")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"members/{id}", member);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
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

            Member? response = await _httpClient.GetFromJsonAsync<Member>($"members/{id}");
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // POST: Members/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"members/{id}");
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Member member)
        {
            if (ModelState.IsValid)
            {
                // Send the HTTP GET request to the login endpoint with email and password as query parameters
                HttpResponseMessage response = await _httpClient.GetAsync($"members/Login?email={member.Email}&password={member.Password}");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Redirect to the desired action if login is successful (e.g., Index)
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the case where login fails, you can add error handling logic here
                    ModelState.AddModelError(string.Empty, "Invalid email or password. Please try again.");
                }
            }
            return View(member);
        }


    }
}
