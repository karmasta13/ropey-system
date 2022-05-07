using Microsoft.AspNetCore.Mvc;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Models.ViewModels;

namespace RopeyDVDSystem.Controllers
{
    public class ReturnController: Controller
    {
        private readonly ApplicationDbContext _context;

        public ReturnController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ReturnModel> GetAllLoanRecords()
        {
            // The DVD copt that are loan list
            IEnumerable<ReturnModel> loanRecord = from dt in _context.DVDTitles
                                                     join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                                     join l in _context.Loans on dc.CopyNumber equals l.CopyNumber
                                                     join m in _context.Members on l.MemberNumber equals m.MemberNumber
                                                     where l.DateReturn == DateTime.MinValue
                                                     orderby l.DateOut descending, dt.DVDTitleName
                                                     select new ReturnModel
                                                     {
                                                         CopyNumber = dc.CopyNumber,
                                                         DVDTitleName = dt.DVDTitleName,
                                                         DateOut = l.DateOut,
                                                         DateDue = l.DateDue,
                                                         MemberName = m.MemberFirstName + ' ' + m.MemberLastName,
                                                         TotalLoan = (int)(from la in _context.Loans
                                                                           join dc in _context.DVDCopies on l.CopyNumber equals dc.CopyNumber
                                                                           where la.DateOut == l.DateOut
                                                                           select la.LoanNumber).Count(),
                                                         LoanNumber = l.LoanNumber
                                                     };

            
            return loanRecord;
        }

        public IActionResult Index()
        {
           
            IEnumerable<ReturnModel> loanRecord = GetAllLoanRecords();

            ViewBag.LoanedCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Select(x => x.CopyNumber).Distinct().ToList());

            return View(loanRecord);
        }
    }

}
