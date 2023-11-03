using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            var loggedInUserName = Request.Cookies["LoggedInUserName"];
            ViewBag.LoggedInUserName = loggedInUserName;

            return View();
        }
    }
}
