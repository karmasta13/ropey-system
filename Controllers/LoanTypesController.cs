using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class LoanTypesController : Controller
    {
        //getting the database context to the controller
        private readonly ApplicationDbContext _context;

        //defining a constructor
        public LoanTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allLoanTypes = await _context.LoanTypes.ToListAsync();
            return View(allLoanTypes);
        }
    }
}
