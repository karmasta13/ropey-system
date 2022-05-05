using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopeyDVDSystem.Models;
using System.Diagnostics;
using RopeyDVDSystem.Data; 


namespace RopeyDVDSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private ApplicationDbContext _context;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }
        
        public IActionResult Index()
        {
            int MaximumUnits = 5;
            ViewBag.MemberCount = _context.Members.Count();
            ViewBag.DVDCount = _context.DVDCopies.Count();
            ViewBag.LoanCount = _context.Loans.Count();
            ViewBag.ActorCount = _context.Actors.Count();

            /*Total DVD based on category  using LinQ and Lambda Expression*/
            var dvdCategoryCounts = (from dvdTitle in _context.DVDTitles
                                     group dvdTitle by dvdTitle.DVDCategory.CategoryName into dvdCategoryGroup
                                     select new { Category = dvdCategoryGroup.Key, Count = dvdCategoryGroup.Count() })
                                    .OrderBy(x => x.Count).Reverse().Take(MaximumUnits);

            List<string> dVDCategoryLabels = dvdCategoryCounts.Select(x => x.Category).ToList();
            ViewBag.DVDCategoryLabels = System.Text.Json.JsonSerializer.Serialize(dVDCategoryLabels);

            List<int> dvdCategoryData = dvdCategoryCounts.Select(x => x.Count).ToList();
            ViewBag.DVDCategoryData = System.Text.Json.JsonSerializer.Serialize(dvdCategoryData);


            var months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            months = months.Take(DateTime.Now.Month).ToArray();
            var currentMonthName = months[DateTime.Now.Month - 1];
            var currentYear = DateTime.Now.Year;

            var loansByMonth = new List<int>();
            for (int i = 0; i < months.Length; i++)
            {
                var month = months[i];
                var loans = _context.Loans.Where(x => x.DateOut.Month == i + 1 && x.DateOut.Year == currentYear).Sum(x => x.ReturnAmount);
                loansByMonth.Add((int)Math.Ceiling((decimal)loans));
            }

            ViewBag.LoanLabels = (string)System.Text.Json.JsonSerializer.Serialize(months);
            ViewBag.LoanData = (string)System.Text.Json.JsonSerializer.Serialize(loansByMonth);

            return View();


            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}