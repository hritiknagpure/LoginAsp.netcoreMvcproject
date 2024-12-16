using System.Diagnostics;
using LoginFormASPcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace LoginFormASPcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;

        public HomeController(MyDbContext context)
        {
            _context = context;
        }

        // Index action
        public IActionResult Index()
        {
            return View();
        }

        // Login page
        public IActionResult Login()
        {
            return View();
        }

        // Handle login form submission
        [HttpPost]
        public IActionResult Login(UserTbl user)
        {
            var myUser = _context.UserTbls
                .FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);

            if (myUser != null)
            {
                // Create session
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("Dashboard");
            }

            // Show error message on login failure
            ViewBag.Message = "Invalid email or password. Please try again.";
            return View();
        }

        // Dashboard action
        public IActionResult Dashboard()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            if (userSession != null)
            {
                ViewBag.MySession = userSession;
                return View();
            }

            // Redirect to login if the session is not found
            return RedirectToAction("Login");
        }

        // Logout action
        public IActionResult Logout()
        {
            // Check if the user session exists
            var userSession = HttpContext.Session.GetString("UserSession");
            if (userSession != null)
            {
                // Remove the user session
                HttpContext.Session.Remove("UserSession");
            }

            // Redirect the user to the Login page after logout
            return RedirectToAction("Login");
        }

        // Privacy policy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
