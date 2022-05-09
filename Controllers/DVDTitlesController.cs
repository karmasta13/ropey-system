using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Data.Services;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Controllers
{
    public class DVDTitlesController : Controller
    {
        //getting both services anf database context in the controller
        private readonly IDVDTitlesService _service;
        private readonly ApplicationDbContext _context;

        //defining a constructor
        public DVDTitlesController(IDVDTitlesService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        //get DVDTitles
        public async Task<IActionResult> Index()
        {
            var allDVDTitles = await _service.GetAllAsync(n => n.Producer, n => n.Studio, n => n.DVDCategory);
            return View(allDVDTitles);
        }

        //GET: DVDTitles/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var dVDTitleDetail = await _service.GetDVDTitleByIdAsync(id);
            return View(dVDTitleDetail);
        }

        //Get: DVDTitles/create/id
        public async Task<IActionResult> Create()
        {
            var dDVTitleDropdownsData = await _service.GetNewDVDTitleDropdownsValues();

            ViewBag.Studios = new SelectList(dDVTitleDropdownsData.Studios, "StudioNumber", "StudioName");
            ViewBag.Producers = new SelectList(dDVTitleDropdownsData.Producers, "ProducerNumber", "ProducerName");
            ViewBag.DVDCategories = new SelectList(dDVTitleDropdownsData.DVDCategories, "CategoryNumber", "CategoryName");
            ViewBag.Actors = new SelectList(dDVTitleDropdownsData.Actors, "ActorNumber", "ActorFirstName"); 
                                                                                                              

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(newDVDTitleVM dVDTitle)
        {
            await _service.AddNewDVDTitleAsync(dVDTitle);
            return RedirectToAction(nameof(Index));
        }

        //GET: DVDTitles/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var dVDDetails = await _service.GetDVDTitleByIdAsync(id);
            if (dVDDetails == null) return View("NotFound");

            var response = new newDVDTitleVM()
            {
                DVDNumber = dVDDetails.DVDNumber,
                DVDTitleName = dVDDetails.DVDTitleName,
                DateReleased = dVDDetails.DateReleased,
                CategoryNumber = dVDDetails.CategoryNumber,
                StudioNumber = dVDDetails.StudioNumber,
                ProducerNumber = dVDDetails.ProducerNumber,
                DVDPictureURL = dVDDetails.DVDPictureURL,
                StandardCharge = dVDDetails.StandardCharge,
                PenaltyCharge = dVDDetails.PenaltyCharge,
                ActorNumbers =dVDDetails.CastMembers.Select(n => n.ActorNumber).ToList(),
            };

            var dVDDropdownsData = await _service.GetNewDVDTitleDropdownsValues();
            ViewBag.DVDCategories = new SelectList(dVDDropdownsData.DVDCategories, "CategoryNumber", "CategoryName");
            ViewBag.Producers = new SelectList(dVDDropdownsData.Producers, "ProducerNumber", "ProducerName");
            ViewBag.Studios = new SelectList(dVDDropdownsData.Studios, "StudioNumber", "StudioName");
            ViewBag.Actors = new SelectList(dVDDropdownsData.Actors, "ActorNumber", "ActorFirstName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, newDVDTitleVM dVDTitle)
        {
            //checking the id of the dvd title to be edited
            if (id != dVDTitle.DVDNumber) return View("NotFound");

            await _service.UpdateDVDTitleAsync(dVDTitle);
            return RedirectToAction(nameof(Index));
        }

        //feature 4: listing the dvd details in increasing order of released date
        public async Task<IActionResult> DVDDetailsIndex()
        {
            //Using LINQ to get data of all DVDs 
            var data = from dvdtitle in _context.DVDTitles
                       join dvdcategory in _context.DVDCategories on dvdtitle.CategoryNumber equals dvdcategory.CategoryNumber
                       join studio in _context.Studios on dvdtitle.StudioNumber equals studio.StudioNumber
                       orderby dvdtitle.DateReleased
                       select new
                       {
                           Title = dvdtitle.DVDTitleName,
                           Picture = dvdtitle.DVDPictureURL,
                           Studio = studio.StudioName,
                           Producer = dvdtitle.Producer.ProducerName,
                           Cast = from casts in dvdtitle.CastMembers
                                  join actor in _context.Actors on casts.ActorNumber equals actor.ActorNumber
                                  group actor by new { casts.DVDNumber } into g
                                  select
                                      String.Join(", ", g.OrderBy(c => c.ActorSurname).Select(x => (x.ActorFirstName + " " + x.ActorSurname))),
                           Release = dvdtitle.DateReleased.ToString("dd MMM yyyy"),
                       };
            //Ordering the data by Castmembers
            data.OrderBy(c => c.Cast);
            return View(await data.ToListAsync());
        }

        //feature 13: listing the dvd titles whose copies haven't been loaned in 31 days
        public async Task<IActionResult> UnoccupiedDVDs()
        {
            //Get DateTime of 31 Days Before Today's DateTime
            var differenceDate = DateTime.Now.AddDays(-31);

            //Get all data of Loaned Copies in 31 Days
            var loanedCopyDetails = (from loan in _context.Loans
                                      where loan.DateOut >= differenceDate
                                      select loan.CopyNumber).Distinct();

            //Get all data of Copies that have not been loaned.
            var notLoanedCopyDetails = from copy in _context.DVDCopies
                                       join dvdtitle in _context.DVDTitles on copy.DVDNumber equals dvdtitle.DVDNumber
                                       join category in _context.DVDCategories on dvdtitle.CategoryNumber equals category.CategoryNumber

                                       where !(loanedCopyDetails).Contains(copy.CopyNumber)
                                       select new
                                       {
                                           CopyNumber = copy.CopyNumber,
                                           Title = dvdtitle.DVDTitleName,
                                           Picture = dvdtitle.DVDPictureURL,
                                           ReleaseDate = dvdtitle.DateReleased.ToString("dd MMM yyyy"),
                                           Category = category.CategoryName,
                                           LastLoan = (from loan in _context.Loans
                                                       join dvdCopy in _context.DVDCopies on loan.CopyNumber equals dvdCopy.CopyNumber
                                                       orderby loan.DateOut descending
                                                       select new
                                                       {
                                                           DateOut = loan.DateOut,
                                                       }
                                                        ).FirstOrDefault()
                                       };

            return View(await notLoanedCopyDetails.ToListAsync());
        }
    }
}
