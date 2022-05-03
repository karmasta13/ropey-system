using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class LoanTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

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
