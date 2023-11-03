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
            if (Request.Cookies["LoggedInUserName"] != null)
            {
                return RedirectToAction("Index", "Dashboard", new { username = Request.Cookies["LoggedInUserName"] });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string password)
        {
            var user = _context.UserAccount.FirstOrDefault(u => u.Username == Username);

            if (user == null || !Verify(password, user.Password))
            {
                ViewBag.ErrorMessage = "Username or password is incorrect."; // Set error message in ViewBag
                return View();
            }

            Response.Cookies.Append("LoggedInUserName", user.Username, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddHours(1),
                HttpOnly = true
            });

            return RedirectToAction("Index", "Dashboard", new { username = user.Username });
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            TempData["LogoutSuccess"] = true;
            Response.Cookies.Delete("LoggedInUserName");
            return RedirectToAction("Login");
        }

    }
}
