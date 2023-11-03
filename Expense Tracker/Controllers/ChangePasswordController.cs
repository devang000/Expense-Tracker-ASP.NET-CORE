using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
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
            var loggedInUserName = Request.Cookies["LoggedInUserName"];
            ViewBag.LoggedInUserName = loggedInUserName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            var loggedInUserName = Request.Cookies["LoggedInUserName"];
            ViewBag.LoggedInUserName = loggedInUserName;

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = _context.UserAccount.FirstOrDefault(u => u.Username == loggedInUserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.OldPassword, user.Password))
            {
                ModelState.AddModelError("OldPassword", "Old password is incorrect");
                return View("Index", model);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            _context.UserAccount.Update(user);
            await _context.SaveChangesAsync();

            TempData["ChangePasswordSuccess"] = true;
            return RedirectToAction("Index", "ChangePassword", new { username = loggedInUserName });
        }
    }
}
