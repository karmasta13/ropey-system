using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class CastMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CastMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allCastMembers = await _context.CastMembers.ToListAsync();
            return View();
        }
    }
}
