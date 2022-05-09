using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;
using RopeyDVDSystem.Data.Services;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Controllers
{
    public class ActorsController : Controller
    {
        //injecting service to the controller
        private readonly IActorsService _service;

        //defining constructor
        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        //Get: /Actors
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

        //Get: Actors/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetActorAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }
        
        //Get: Actors/delete/id
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
