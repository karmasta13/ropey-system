using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class DVDCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DVDCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allDVDCategories = await _context.DVDCategories.ToListAsync();
            return View(allDVDCategories);
        }
    }
}
