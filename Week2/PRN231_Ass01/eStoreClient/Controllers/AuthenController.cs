using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace eStoreClient2.Controllers
{
    public class AuthenController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AuthenController(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7254/api/")
            };
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }

        // POST: Members/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Member member)
        {
            if (string.IsNullOrWhiteSpace(member.Email) || string.IsNullOrWhiteSpace(member.Password))
            {
                ModelState.AddModelError(string.Empty, "Email and Password are required.");
                return View(member);
            }
            string? adminEmail = _configuration["Admin:Email"];
            string? adminPassword = _configuration["Admin:Password"];
            if (member.Email == _configuration["Admin:Email"] && member.Password == _configuration["Admin:Password"])
            {
                // Set a flag or role in the session to identify the user as admin
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("CurrentUser", "Admin");

                return RedirectToAction("Index", "Home");
            }


            HttpResponseMessage response = await _httpClient.GetAsync($"members/Login?email={member.Email}&password={member.Password}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                Member? loggedInMember = JsonConvert.DeserializeObject<Member>(content);

                HttpContext.Session.SetString("CurrentUser", member.Email);
                return RedirectToAction("Index", "Products");
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                ModelState.Clear();
                ModelState.AddModelError("Email", "Invalid email or password. Please try again.");
            }

            return View(member);
        }

        public IActionResult Register()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("MemberId,Email,Password,CompanyName,City,Country")] Member member)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("members", member);
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    HttpContext.Session.SetString("CurrentUser", member.Email);
                    return RedirectToAction("Index", "Products");
                }
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "This Email is already existed!");
            }
            return View(member);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
