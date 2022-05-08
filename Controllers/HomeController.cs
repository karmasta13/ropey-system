using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Models;
using RopeyDVDSystem.Models.ViewModels;
using System.Diagnostics;

namespace RopeyDVDSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context; 

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
       


        public IActionResult Index()
        {
          
            var searchBarValue = HttpContext.Session.GetString("searchBarValue");
            var sortingOrderCol = HttpContext.Session.GetString("sortingOrderCol");
            var stockAvailability  = HttpContext.Session.GetString("stockAvailability");
            var ageRestricted = HttpContext.Session.GetString("AgeRestricted");

            var databaseDVDList = from dvd in _context.DVDTitles select dvd.DVDNumber;


            switch (ageRestricted)
            {
                case "yes":
                    databaseDVDList = from dvd in databaseDVDList
                                      join dvddt in _context.DVDTitles on dvd equals dvddt.DVDNumber
                                      join dc in _context.DVDCategories on dvddt.CategoryNumber equals dc.CategoryNumber
                                      where dc.AgeRestricted == "False"
                                      select dvd;
                    break;
            }



            ViewBag.sortingOrderCol = string.IsNullOrEmpty(sortingOrderCol) ? "na" : sortingOrderCol;
            ViewBag.stockAvailability  = string.IsNullOrEmpty(stockAvailability ) ? "all" : stockAvailability ;
            ViewBag.AgeRestricted = string.IsNullOrEmpty(ageRestricted) ? "no" : ageRestricted;
           

        
            IEnumerable<HomeDVD> dvdDetails = from allDVD in databaseDVDList
                                                      join dvd in _context.DVDTitles on allDVD equals dvd.DVDNumber
                                                      join category in _context.DVDCategories on dvd.CategoryNumber equals category.CategoryNumber
                                                      select new HomeDVD
                                                      {
                                                          DVDTitleName = dvd.DVDTitleName,
                                                          DVDPictureURL = dvd.DVDPictureURL,
                                                          DVDCategory = category.CategoryName,
                                                          StandardCharge = dvd.StandardCharge,
                                                          DateReleased = dvd.DateReleased,
                                                          CastMember = string.Join(", ",
                                                                                      (from castMember in _context.CastMembers
                                                                                       where castMember.DVDNumber == dvd.DVDNumber
                                                                                       select string.Concat(castMember.Actor.ActorFirstName, " ", castMember.Actor.ActorSurname)
                                                                                      ).ToArray()
                                                                                  ),
                                                          AvailableQuantity = _context.DVDCopies.Where(d => d.DVDNumber == dvd.DVDNumber).Count() == 0 ?
                                                                                  -1 :
                                                                                  (from dvdCopy in _context.DVDCopies
                                                                                   where dvdCopy.DVDNumber == dvd.DVDNumber
                                                                                   select dvdCopy.IsLoan ? 0 : 1).Sum()
                                                      };

            
            if (!string.IsNullOrEmpty(searchBarValue))
            {
                ViewBag.searchBarValue = searchBarValue;
                dvdDetails = dvdDetails.Where(d => d.CastMember.ToLower().Contains(searchBarValue.ToLower()) || d.DVDTitleName.ToLower().Contains(searchBarValue.ToLower()));
            }
         
            
            switch (sortingOrderCol)
            {
                case "pa":
                    dvdDetails = dvdDetails.OrderBy(d => d.StandardCharge);
                    break;

                case "pd":
                    dvdDetails = dvdDetails.OrderByDescending(d => d.StandardCharge);
                    break;


                default:
                    dvdDetails = dvdDetails.OrderBy(d => d.DVDTitleName);
                    break;
            }



            switch (stockAvailability)
            {
                
                case "available":
                    dvdDetails = dvdDetails.Where(d => d.AvailableQuantity > 0);
                    break;

                case "outOfStock":
                    dvdDetails = dvdDetails.Where(d => d.AvailableQuantity == 0);
                    break;

               
            }




            return View(dvdDetails);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostIndex()
        {
            
            HttpContext.Session.SetString("searchBarValue", Request.Form["searchBarValue"]);
            HttpContext.Session.SetString("sortingOrderCol", Request.Form["sortingOrderCol"]);
            HttpContext.Session.SetString("stockAvailability", Request.Form["stockAvailability"]);
            HttpContext.Session.SetString("AgeRestricted", Request.Form["AgeRestricted"]);


            return RedirectToAction("Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}