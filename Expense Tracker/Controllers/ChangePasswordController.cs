using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net; // Import the BCrypt.Net namespace
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChangePasswordController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            var loggedInUserName = Request.Cookies["LoggedInUserName"];
            ViewBag.LoggedInUserName = loggedInUserName;

            var user = _context.UserAccount.FirstOrDefault(u => u.Username == loggedInUserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
            {
                ViewBag.ChangePasswordError = "Old password is incorrect."; // Set error message in ViewBag
                return View("ChangePassword"); // Display the ChangePassword view with an error message
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.UserAccount.Update(user);
            await _context.SaveChangesAsync();

            TempData["ChangePasswordSuccess"] = true;
            return RedirectToAction("Index", "Dashboard", new { username = loggedInUserName });
        }
    }
}
