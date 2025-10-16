using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult AboutCookies()
        {
            return View();
        }
        // Set a sample persistent cookie
        public IActionResult SetSampleCookie()
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(3),
                HttpOnly = true,
                IsEssential = true
            };
            Response.Cookies.Append("DemoCookie", "HelloFromCookieDemo", options);
            ViewBag.Message = "A persistent cookie named 'DemoCookie' has been set! (Expires in 3 days)";
            return View();
        }
        // Read the sample cookie
        public IActionResult ReadSampleCookie()
        {
            string? value = Request.Cookies["DemoCookie"];
            ViewBag.Message = value == null ? "Cookie not found." : $"Cookie value: {value}";
            return View();
        }
        // Delete the sample cookie
        public IActionResult DeleteSampleCookie()
        {
            Response.Cookies.Delete("DemoCookie");
            ViewBag.Message = "Cookie has been deleted!";
            return View();
        }
        // Accessing Cookies in a View using IHttpContextAccessor
        public IActionResult ViewCookiesInView()
        {
            return View();
        }
    }
}
