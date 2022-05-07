using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var allMembers = await _context.Members.Include(n => n.MembershipCategory).ToListAsync();
            //return View(allMembers);

            //Using LINQ to get Member Details
            var allMembers = from members in _context.Members
                                       join membership in _context.MembershipCategories on members.MemberCategoryNumber equals membership.MembershipCategoryNumber
                                       select new
                                       {
                                           MemberNumber = members.MemberNumber,
                                           MemberFirstName = members.MemberFirstName,
                                           MemberLastName = members.MemberLastName,
                                           MemberAddress = members.MemberAddress,
                                           MemberDOB = members.MemberDateOfBirth.ToString("dd MMM yyyy"),
                                           Membership = membership.MembershipCategoryName,
                                           TotalAcceptLoans = membership.MembershipCategoryTotalLoans,
                                           TotalCurrentLoans = (from loans in _context.Loans
                                                                where loans.DateReturn == null
                                                                where loans.MemberNumber == members.MemberNumber
                                                                select loans).Count(),
                                       };
            return View(await allMembers.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Get Member Details
            var memberDetails = await _context.Members
                .Include(m => m.MembershipCategory)
                .FirstOrDefaultAsync(m => m.MemberNumber == id);

            //Get DateTime of 31 Days Before Today's DateTime
            var differenceDate = DateTime.Now.AddDays(-31);
            //Get all data of loan details within the 31 days period
            var data = from member in _context.Members
                       join loan in _context.Loans on member.MemberNumber equals loan.MemberNumber
                       where loan.DateOut >= differenceDate
                       where member.MemberNumber == id
                       join dvdcopy in _context.DVDCopies on loan.CopyNumber equals dvdcopy.CopyNumber
                       join dvdtitle in _context.DVDTitles on dvdcopy.DVDNumber equals dvdtitle.DVDNumber
                       select new
                       {
                           Loan = loan.LoanNumber,
                           CopyNumber = dvdcopy.CopyNumber,
                           Title = dvdtitle.DVDTitleName,
                           DateOut = loan.DateOut.ToString("dd MMM yyyy"),
                           DateReturn = loan.DateReturn.ToString("dd MMM yyyy")
                       };

            if (memberDetails == null)
            {
                return NotFound();
            }

            if (data == null)
            {
                ViewData["Member"] = memberDetails;
                return View(memberDetails);
            }
            else
            {
                ViewData["Member"] = memberDetails;
                return View(data);
            }


        }
    }
}
