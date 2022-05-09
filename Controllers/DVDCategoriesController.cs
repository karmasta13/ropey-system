using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Data.Services;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Controllers
{
    public class DVDCategoriesController : Controller
    {
        //injecting services into the controller
        private readonly IDVDCategoriesService _service;

        //defining a constructor
        public DVDCategoriesController(IDVDCategoriesService service)
        {
            _service = service;
        }

        //get DVDCategories
        public async Task<IActionResult> Index()
        {
            var allDVDCategories = await _service.GetAllAsync();
            return View(allDVDCategories);
        }

        //Get: producers/details/id
        public async Task<IActionResult> Details(int id)
        {
            var categoryDetails = await _service.GetDVDCategoryAsync(id);

            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        //Get: DVDCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CategoryName, CategoryDescription, AgeRestricted")] DVDCategory dVDCategory)
        {
            await _service.AddAsync(dVDCategory);
            return RedirectToAction(nameof(Index));
        }

        //Get: DVDCategories/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDetails = await _service.GetDVDCategoryAsync(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryNumber, CategoryName, CategoryDescription, AgeRestricted")] DVDCategory dVDCategory)
        {
            if (id == dVDCategory.CategoryNumber)
            {
                await _service.UpdateAsync(id, dVDCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(dVDCategory);
        }

        //Get: Actors/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDetails = await _service.GetDVDCategoryAsync(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryDetails = await _service.GetDVDCategoryAsync(id);
            if (categoryDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
