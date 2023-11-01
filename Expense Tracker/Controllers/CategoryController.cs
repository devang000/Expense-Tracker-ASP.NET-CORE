using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;

namespace Expense_Tracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
        }

        

        // GET: Category/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
                return View(new Category());
            else
                return View(_context.Categories.Find(id));
        }

        // POST: Category/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type,Budget")] Category category)
        {
            if (ModelState.IsValid)
            {
                    if (category.CategoryId == 0)
                        _context.Add(category);
                    else
                        _context.Update(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
                return View(category);
        }

        ////HttpGetAttribute budget for displaying budget in index
        //public IActionResult GetBudget(int CategoryId)
        //{
        //    // Fetch the budget information for the specified category from the database
        //    var category = _context.Categories.Include(c => c.Budget).FirstOrDefault(c => c.CategoryId == CategoryId);

        //    if (category != null)
        //    {
        //        decimal budgetAmount = category.Budget; // Assuming you have a Budget property with an Amount field
        //        return Json(budgetAmount); // Return the budget amount as JSON
        //    }
        //    else
        //    {
        //        return Json("Budget not found");
        //    }
        //}

        //[HttpGet("GetCategoryName/{categoryId}")]
        //public IActionResult GetCategoryName(int categoryId)
        //{
        //    var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

        //    if (category != null)
        //    {
        //        string categoryName = category.Title; // Assuming 'Title' is the property that stores the category name
        //        return Ok(categoryName);
        //    }
        //    else
        //    {
        //        return NotFound(); // Or return an appropriate status code for "not found"
        //    }
        //}



        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> GetBudget(int CategoryId)
        //{
        //    var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == CategoryId);

        //    if (CategoryId != null)
        //    {

        //            decimal budgetAmount = category.Budget;
        //            return Json(new { Budget = budgetAmount });

        //    }
        //    else
        //    {
        //        return Json(new { Message = "CategoryID not found" });
        //    }
        //}

        // GET: Category/GetBudget
        public async Task<IActionResult> GetBudget(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Json(category.Budget);
        }
    }
}
