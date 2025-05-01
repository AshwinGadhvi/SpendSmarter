using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmarter.Models;

namespace SpendSmarter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpendSmartDbContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();
            var totalExpenses = allExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;
            return View(allExpenses);
        }
        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                var expense = _context.Expenses.Find(id);
                if (expense != null)
                {
                    return View(expense);
                }
            }
            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense Model)
        {
            if(Model.Id == 0)
            {

                _context.Expenses.Add(Model);
            }
            else
            {
                _context.Expenses.Update(Model);
            }
            _context.SaveChanges();
            return Redirect("Expenses");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
