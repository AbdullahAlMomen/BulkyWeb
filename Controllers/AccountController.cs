using BulkyWeb.Models.ViewModel;
using BulkyWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserStoreService _userStoreService;
        private const string CookieUserId = "UserId";
        private const string CookieUsername = "Username";
        public AccountController(UserStoreService userStoreService)
        {
            _userStoreService = userStoreService;
        }
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (Request.Cookies.ContainsKey(CookieUserId))
                return RedirectToAction("Dashboard", "Account");
            return View(new LoginViewModel());
        }
        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = _userStoreService.ValidateUser(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
            // Set Cookie Options
            var options = new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                IsEssential = true,
                Secure = true // Set to true in production (requires HTTPS)
            };
            if (model.RememberMe)
            {
                // Persistent cookie (expires in 7 days)
                options.Expires = DateTime.UtcNow.AddDays(7);
            }
            else
            {
                // Non-persistent (Session cookie) - no expiration set, deleted on browser close
                options.Expires = null; // Or omit this line
            }
            // Create Cookies
            Response.Cookies.Append(CookieUserId, user.Id.ToString(), options);
            Response.Cookies.Append(CookieUsername, user.Username, options);
            return RedirectToAction("Dashboard", "Account");
        }
        // GET: /Account/Dashboard
        public IActionResult Dashboard()
        {
            if (!Request.Cookies.ContainsKey(CookieUserId))
                return RedirectToAction("Login");
            if (!int.TryParse(Request.Cookies[CookieUserId], out int userId))
                return RedirectToAction("Login");
            var user = _userStoreService.GetUserById(userId);
            if (user == null)
                return RedirectToAction("Login");
            return View(user);
        }
        // GET: /Account/Logout
        public IActionResult Logout()
        {
            Response.Cookies.Delete(CookieUserId);
            Response.Cookies.Delete(CookieUsername);
            return RedirectToAction("Login");
        }
    }
}
