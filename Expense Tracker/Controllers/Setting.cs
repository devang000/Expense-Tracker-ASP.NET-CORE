using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class Setting : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
