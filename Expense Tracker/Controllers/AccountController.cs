using static BCrypt.Net.BCrypt;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                var existinguser = _context.UserAccount.FirstOrDefault(u => u.Username == user.Username);
                if (existinguser != null)
                {
                    ModelState.AddModelError("username", "username already exists");
                    return View(user);
                }
                user.Password = HashPassword(user.Password);
                    _context.UserAccount.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }


        // AccountController.cs

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string password)
        {
            var user = _context.UserAccount.FirstOrDefault(u => u.Username == Username);

            if (user != null && Verify(password, user.Password))
            {
                // Set the username in a cookie
                Response.Cookies.Append("LoggedInUserName", user.Username, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddHours(1), // Set cookie expiration time
                    HttpOnly = true // Helps to prevent XSS attacks by restricting access from JavaScript
                });

                // Redirect to the DashboardController with the username parameter
                return RedirectToAction("Index", "Dashboard", new { username = user.Username });
            }

            ModelState.AddModelError("Login", "Invalid username or password");
            return View();
        }




        public async Task<IActionResult> Logout()
        {
            // Clear the specific cookie that stores the logged-in user's information
            Response.Cookies.Delete("LoggedInUserName");

            // Redirect to the login page or any other relevant page after logout
            return RedirectToAction("Login");
        }

    }
}
