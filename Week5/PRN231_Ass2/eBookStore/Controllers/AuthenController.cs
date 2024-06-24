using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace eBookStore.Controllers
{
    public class AuthenController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AuthenController(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7182/odata/")
            };
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] User member)
        {
            if (string.IsNullOrWhiteSpace(member.Email) || string.IsNullOrWhiteSpace(member.Password))
            {
                ModelState.AddModelError(string.Empty, "Email and Password are required.");
                return View(member);
            }
            if (member.Email == _configuration["Admin:Email"] && member.Password == _configuration["Admin:Password"])
            {
                // Set a flag or role in the session to identify the user as admin
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("CurrentUser", "Admin");


                return RedirectToAction("Index", "Home"); // Redirect to admin dashboard or wherever you want admin users to go.
            }


            HttpResponseMessage response = await _httpClient.GetAsync($"User?filter=Email eq '{member.Email}' and Password eq '{member.Password}'");

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

                if (users.Count() > 0)
                {
                    // Login successful, store user information in session
                    User loggedInMember = users.First(); // Assuming a unique user is found
                    HttpContext.Session.SetString("CurrentUser", loggedInMember.Email);
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                // Handle the case where the API returned no content
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
        public async Task<IActionResult> Register([Bind("UserId,Email,Password,FirstName,MiddleName,LastName,Source")] User member)
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

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
