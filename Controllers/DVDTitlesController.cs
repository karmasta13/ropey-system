using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class DVDTitlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DVDTitlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allDVDTitles = await _context.DVDTitles.Include(n => n.Producer).Include(n => n.Studio).Include(n => n.DVDCategory).ToListAsync();
            return View(allDVDTitles);
        }
    }
}
