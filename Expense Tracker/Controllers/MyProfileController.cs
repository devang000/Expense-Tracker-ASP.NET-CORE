using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var loggedInUserName = Request.Cookies["LoggedInUserName"];
            ViewBag.LoggedInUserName = loggedInUserName;

            // Assuming you have a model for users in your ApplicationDbContext, you can fetch the user details
            var user = _context.UserAccount.FirstOrDefault(u => u.Username == loggedInUserName);

            if (user != null)
            {
                return View(user); // Pass the user details to the view
            }

            return View(); // Return the view if user not found
        }

        [HttpPost]
        [HttpPost]
        public IActionResult UpdateProfile(UserAccount updatedUser)
        {
            var existingUser = _context.UserAccount.FirstOrDefault(u => u.idUser == updatedUser.idUser);

            if (existingUser != null)
            {
                try
                {
                    // Update the user details with the new values
                    existingUser.FirstName = updatedUser.FirstName;
                    existingUser.LastName = updatedUser.LastName;
                    existingUser.Email = updatedUser.Email;
                    existingUser.Username = updatedUser.Username;
                    // ... Update other properties as needed

                    _context.SaveChanges(); // Save changes to the database

                    // Show success message using TempData for the redirect to Index
                    TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log error to console
                    Console.WriteLine("Error: " + ex.Message);
                    // You can also use a logging framework like Serilog or ILogger to log errors
                }
            }

            // Show error using TempData if the user is not found
            TempData["ErrorMessage"] = "An error occurred while updating your profile";
            return View("Error");
        }


    }
}
