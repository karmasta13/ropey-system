using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Data.Services;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allActors = await _service.GetAllAsync();
            return View(allActors);
        }

        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ActorFirstName,ActorPictureURL,ActorSurname")] Actor actor)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/details/Id
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetActorAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        //Get: Actors/edit
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetActorAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            //actor.ActorNumber = Convert.ToUInt32(id);
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }
        
        //Get: Actors/delete
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetActorAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetActorAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
