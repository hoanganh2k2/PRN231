using Microsoft.AspNetCore.Mvc;

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
				BaseAddress = new Uri("https://localhost:7025/api/")
			};
			_configuration = configuration;
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(string email, string pass)
		{
			if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
			{
				ModelState.AddModelError(string.Empty, "Email and Password are required.");
				return View(ModelState);
			}
			if (email == _configuration["Admin:Email"] && pass == _configuration["Admin:Password"])
			{
				// Set a flag or role in the session to identify the user as admin
				HttpContext.Session.SetString("IsAdmin", "true");
				HttpContext.Session.SetString("CurrentUser", "Admin");


				return RedirectToAction("Index", "Home"); // Redirect to admin dashboard or wherever you want admin users to go.
			}
			ModelState.AddModelError(string.Empty, "Email and Password are not admin account.");
			return View(ModelState);
		}

		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}
