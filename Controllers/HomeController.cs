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
            // Get all the request DVD information from the database
            var dvdDetails = from dvd in _context.DVDTitles
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
                                                             ).ToList()
                                                         ),
                                 AvailableQuantity = _context.DVDCopies.Where(d => d.DVDNumber == dvd.DVDNumber).Count() == 0 ?
                                                        -1 :
                                                        (from dvdCopy in _context.DVDCopies
                                                         where dvdCopy.DVDNumber == dvd.DVDNumber
                                                         select dvdCopy.IsLoan ? 0 : 1).Sum()
                             
                              };
            return View(dvdDetails);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}