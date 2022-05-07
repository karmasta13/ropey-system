using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Data.Services;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }

        //Get: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProducerPictureURL,ProducerName")] Producer producer)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(producer);
            //}
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //Get: Producers/details/Id
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetProducerAsync(id);

            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        //Get: Producers/edit
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetProducerAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(producer);
            //}
            //producer.ProducerNumber = Convert.ToUInt32(id);
            await _service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        //Get: Producers/delete
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetProducerAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetProducerAsync(id);
            if (producerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
