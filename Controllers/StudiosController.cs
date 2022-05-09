using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Data.Services;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Controllers
{
    public class StudiosController : Controller
    {
        //injecting service to the controller
        private readonly IStudiosService _service;

        //creating a constructor
        public StudiosController(IStudiosService service)
        {
            _service = service;
        }

        //get Studio
        public async Task<IActionResult> Index()
        {
            var allStudios = await _service.GetAllAsync();
            return View(allStudios);
        }

        //Get: producers/details/id
        public async Task<IActionResult> Details(int id)
        {
            var studioDetails = await _service.GetStudioAsync(id);

            if (studioDetails == null) return View("NotFound");
            return View(studioDetails);
        }

        //Get: DVDCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("StudioName")] Studio studio)
        {

            await _service.AddAsync(studio);
            return RedirectToAction(nameof(Index));
        }

        //Get: DVDCategories/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var studioDetails = await _service.GetStudioAsync(id);
            if (studioDetails == null) return View("NotFound");
            return View(studioDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("StudioNumber, StudioName")] Studio studio)
        {
            if (id == studio.StudioNumber)
            {
                await _service.UpdateAsync(id, studio);
                return RedirectToAction(nameof(Index));
            }
            return View(studio);
        }

        //Get: Actors/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var studioDetails = await _service.GetStudioAsync(id);
            if (studioDetails == null) return View("NotFound");
            return View(studioDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studioDetails = await _service.GetStudioAsync(id);
            if (studioDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
