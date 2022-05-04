using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class MembershipCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allMembershipCategories = await _context.MembershipCategories.ToListAsync();
            return View(allMembershipCategories);
        }
    }
}
