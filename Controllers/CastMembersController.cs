using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class CastMembersController : Controller
    {
        //getting database context in controller
        private readonly ApplicationDbContext _context;

        //defining a constructor
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
