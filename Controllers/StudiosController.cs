using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class StudiosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudiosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allStudios = await _context.Studios.ToListAsync();
            return View(allStudios);
        }
    }
}
