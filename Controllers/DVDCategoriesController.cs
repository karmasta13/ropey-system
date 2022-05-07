using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Data.Services;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Controllers
{
    public class DVDCategoriesController : Controller
    {
        private readonly IDVDCategoriesService _service;

        public DVDCategoriesController(IDVDCategoriesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allDVDCategories = await _service.GetAllAsync();
            return View(allDVDCategories);
        }

        //Get: producers/details/1
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
            //if (!ModelState.IsValid) return View(dVDCategory);
            
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
            //if (!ModelState.IsValid) return View(dVDCategory);
            if (id == dVDCategory.CategoryNumber)
            {
                await _service.UpdateAsync(id, dVDCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(dVDCategory);
        }

        //Get: Actors/delete
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
