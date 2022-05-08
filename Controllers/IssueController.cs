using Microsoft.AspNetCore.Mvc;
using RopeyDVDSystem.Models.ViewModels;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Controllers
{
    public class IssueController : Controller
    {

        private readonly ApplicationDbContext _context;

        public IssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IssueModel> GetAllAvailableCopy()
        {
            IEnumerable<IssueModel> loanRecord = (from dt in _context.DVDTitles
                                                 join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                                 join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                                 where dc.IsLoan == false
                                                 select new IssueModel
                                                 {
                                                     CopyNumber = dc.CopyNumber,
                                                     DVDTitleName = dt.DVDTitleName,
                                                     DVDCategory = dtc.CategoryName,
                                                     AgeRestricted = dtc.AgeRestricted,
                                                     DateReleased = dt.DateReleased
                                                 });
            return loanRecord;
        }

        public IActionResult Index()
        {
  
            IEnumerable<IssueModel> availableCopy = GetAllAvailableCopy();

            ViewBag.AvailableCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Where(x => x.IsLoan == false).Select(x => x.CopyNumber).Distinct().ToList());

            return View(availableCopy);
        }

        // for search functionality
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string request)
        {
            string CopyNumber = Request.Form["SearchCopyNumber"];
            ViewBag.SearchCopyNumber = CopyNumber;

            ViewBag.AvailableCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Where(x => x.IsLoan == false).Select(x => x.CopyNumber).Distinct().ToList());

            if (CopyNumber != null &&
                int.TryParse(CopyNumber, out int copyNumber) &&
                _context.DVDCopies.Where(x => x.CopyNumber == copyNumber).Count() > 0)
            {
                var loanRecord = (from dt in _context.DVDTitles
                                  join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                  join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                  where dc.IsLoan == false && dc.CopyNumber == copyNumber
                                  select new IssueModel
                                  {
                                      CopyNumber = dc.CopyNumber,
                                      DVDTitleName = dt.DVDTitleName,
                                      DVDCategory = dtc.CategoryName,
                                      AgeRestricted = dtc.AgeRestricted,
                                      DateReleased = dt.DateReleased
                                  });
                return View(loanRecord);
            }
            else if (CopyNumber == "")
            {
                IEnumerable<IssueModel> loanRecord = GetAllAvailableCopy();
                return View(loanRecord);
            }
            else
            {
                return View();
            }

        }

        public IActionResult Create(int id)
        {
            if (id == 0 || _context.DVDCopies.Where(l => l.CopyNumber == id && l.IsLoan == false).Count() == 0) { return RedirectToAction("Index"); }

     
            IssueModel currentLoan = (from dt in _context.DVDTitles
                                     join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                     join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                     where dc.CopyNumber == id
                                     select new IssueModel
                                     {
                                         CopyNumber = dc.CopyNumber,
                                         DVDTitleName = dt.DVDTitleName,
                                         DVDCategory = dtc.CategoryName,
                                         AgeRestricted = dtc.AgeRestricted
                                     }).First();

            ViewData["MemberNumberList"] = from m in _context.Members select new SelectViewModel { SelectValue = (int)m.MemberNumber, SelectKey = m.MemberFirstName + " " + m.MemberLastName };
            ViewData["LoanTypeList"] = from lt in _context.LoanTypes select new SelectViewModel { SelectValue = (int)lt.LoanTypeNumber, SelectKey = lt.LoanTypeName };
            ViewData["AvailableDVD"] = currentLoan;
            ViewBag.CopyNumber = currentLoan.CopyNumber;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("LoanTypeNumber, CopyNumber, MemberNumber")] IssueModel rent)
        {
            var test = rent;
           
            IssueModel currentLoan = (from dt in _context.DVDTitles
                                     join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                     join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                     where dc.CopyNumber == rent.CopyNumber
                                     select new IssueModel
                                     {
                                         CopyNumber = dc.CopyNumber,
                                         DVDTitleName = dt.DVDTitleName,
                                         DVDCategory = dtc.CategoryName,
                                         AgeRestricted = dtc.AgeRestricted
                                     }).First();

            ViewData["MemberNumberList"] = from m in _context.Members select new SelectViewModel { SelectValue = (int)m.MemberNumber, SelectKey = m.MemberFirstName + " " + m.MemberLastName };
            ViewData["LoanTypeList"] = from lt in _context.LoanTypes select new SelectViewModel { SelectValue = (int)lt.LoanTypeNumber, SelectKey = lt.LoanTypeName };
            ViewData["AvailableDVD"] = currentLoan;

            ViewBag.LoanTypeNumber = rent.LoanTypeNumber;
            ViewBag.MemberNumber = rent.MemberNumber;
            ViewBag.CopyNumber = rent.CopyNumber;

            string operation;
            try
            {
                operation = Request.Form["GenerateButton"];
            }
            catch
            {
                operation = "";
            }

         
            string ageRestriction = (from dc in _context.DVDCopies
                                   join d in _context.DVDTitles on dc.DVDNumber equals d.DVDNumber
                                   join dtc in _context.DVDCategories on d.CategoryNumber equals dtc.CategoryNumber
                                   where dc.CopyNumber == rent.CopyNumber
                                   select dtc.AgeRestricted).First();

            if (ageRestriction == "True" && (DateTime.Today - _context.Members.Where(m => m.MemberNumber == rent.MemberNumber).First().MemberDateOfBirth).Days / 365 < 18)
            {
                TempData["Error"] = "Below age restriction";
                return View();
            }

          
            int maxRentLimit = (from m in _context.Members
                                 join mt in _context.MembershipCategories on m.MemberCategoryNumber equals mt.MembershipCategoryNumber
                                 where m.MemberNumber == rent.MemberNumber
                                 select mt.MembershipCategoryTotalLoans).FirstOrDefault();

            if (_context.Loans.Where(l => l.DateReturn == DateTime.MinValue && l.MemberNumber == rent.MemberNumber).Count() >= maxRentLimit)
            {
                TempData["Error"] = "Maximum number reached";
                return View();
            }


         
            int loanDays = (from lt in _context.LoanTypes
                             where lt.LoanTypeNumber == rent.LoanTypeNumber
                             select lt.Duration).First();

            ReturnModel returnModel = (from dt in _context.DVDTitles
                                          join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                          join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                          where dc.CopyNumber == rent.CopyNumber
                                          select new ReturnModel
                                          {
                                              CopyNumber = dc.CopyNumber,
                                              DVDTitleName = dt.DVDTitleName,
                                              DVDCategory = dtc.CategoryName,
                                              DateOut = DateTime.Today,
                                              DateDue = DateTime.Today.AddDays(loanDays),
                                              MemberName = _context.Members.Where(m => m.MemberNumber == rent.MemberNumber).First().MemberFirstName + " " + _context.Members.Where(m => m.MemberNumber == rent.MemberNumber).First().MemberLastName,
                                              StandardCharge = dt.StandardCharge,
                                              Payment = dt.StandardCharge * loanDays,
                                          }).First();
            ViewData["RentalInformation"] = returnModel;

            if (operation != "Generate")
            {
                // add a new loan record
                Loan loan = new Loan
                {
                    CopyNumber = rent.CopyNumber,
                    MemberNumber = (int)rent.MemberNumber,
                    LoanTypeNumber = (int)rent.LoanTypeNumber,
                    DateOut = DateTime.Today,
                    DateDue = DateTime.Today.AddDays(loanDays),
                    DateReturn = DateTime.MinValue,
                    ReturnAmount = returnModel.Payment
                };
                _context.Loans.Add(loan);

                // Put the copy status to unavailable
                var Copy = _context.DVDCopies.Where(c => c.CopyNumber == rent.CopyNumber).First();
                Copy.IsLoan = true;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
