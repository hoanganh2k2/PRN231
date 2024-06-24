using Microsoft.AspNetCore.Mvc;

namespace eStoreClient2.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }

        // POST: Members/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string pass)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                ModelState.AddModelError(string.Empty, "Email and Password are required.");
                return View();
            }
            string? adminEmail = _configuration["Admin:Email"];
            string? adminPassword = _configuration["Admin:Password"];
            if (email == adminEmail && pass == adminPassword)
            {
                // Set a flag or role in the session to identify the user as admin
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("CurrentUser", "Admin");

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
