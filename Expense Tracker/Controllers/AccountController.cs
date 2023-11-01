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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Check if the username and password are valid
            var user = _context.UserAccount.FirstOrDefault(u => u.Username == username);
            if (user == null || !Verify(password, user.Password))
            {
                ModelState.AddModelError("Login", "Invalid username or password");
                return View();
            }

            // Redirect the user to the home page
            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            // Clear the user session
            HttpContext.Session.Clear();

            // Redirect the user to the login page
            return RedirectToAction("Login");
        }
    }
}
