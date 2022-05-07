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
        private readonly IDVDTitlesService _service;
        private readonly ApplicationDbContext _context;

        public DVDTitlesController(IDVDTitlesService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allDVDTitles = await _service.GetAllAsync(n => n.Producer, n => n.Studio, n => n.DVDCategory);
            return View(allDVDTitles);
        }

        //GET: DVDTitles/Details/id
        //[AllowAnonymous]
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
            //if (!ModelState.IsValid)
            //{
            //    var movieDropdownsData = await _service.GetNewDVDTitleDropdownsValues();

            //    ViewBag.Cinemas = new SelectList(movieDropdownsData.DVDCategories, "CategoryNumber", "CategoryName");
            //    ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "ProducerNumber", "ProducerName");
            //    ViewBag.Producers = new SelectList(movieDropdownsData.Studios, "StudioNumber", "StudioName");
            //    ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "ActorNumber", "ActorFirstName");

            //    return View(dVDTitle);
            //}

            await _service.AddNewDVDTitleAsync(dVDTitle);
            return RedirectToAction(nameof(Index));
        }

        //GET: Movies/Edit/1
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
            if (id != dVDTitle.DVDNumber) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var dVDDropdownsData = await _service.GetNewDVDTitleDropdownsValues();

                ViewBag.Cinemas = new SelectList(dVDDropdownsData.DVDCategories, "CategoryNumber", "CategoryName");
                ViewBag.Producers = new SelectList(dVDDropdownsData.Producers, "ProducerNumber", "ProducerName");
                ViewBag.Producers = new SelectList(dVDDropdownsData.Studios, "StudioNumber", "StudioName");
                ViewBag.Actors = new SelectList(dVDDropdownsData.Actors, "ActorNumber", "ActorFirstName");

                return View(dVDTitle);
            }

            await _service.UpdateDVDTitleAsync(dVDTitle);
            return RedirectToAction(nameof(Index));
        }


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
            //Ordering the data by Cast
            data.OrderBy(c => c.Cast);
            return View(await data.ToListAsync());
        }
    }
}
