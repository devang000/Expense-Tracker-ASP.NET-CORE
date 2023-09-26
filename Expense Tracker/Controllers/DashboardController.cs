﻿using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> SelectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();


            //Total Income

            int TotalIncome = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("c0");

            //Total Expense
            int TotalExpense= SelectedTransactions
             .Where(i => i.Category.Type == "Expense")
             .Sum(j => j.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("c0");

            //balance
            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balamce = Balance.ToString("c0");

            return View();
        }
    }
}
