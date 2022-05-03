using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.Services
{
	public class ActorsService : IActorsService
	{
		private readonly ApplicationDbContext _context;

		public ActorsService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Actor actor)
		{
			await _context.Actors.AddAsync(actor);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var result = await _context.Actors.FirstOrDefaultAsync(n => n.ActorNumber == id);
			_context.Actors.Remove(result);
			await _context.SaveChangesAsync();
		}

		public async Task<Actor> GetActorAsync(int id)
		{
			var result = await _context.Actors.FirstOrDefaultAsync(n => n.ActorNumber == id);
			return result;
		}

		public async Task<IEnumerable<Actor>> GetAllAsync()
		{
			var result = await _context.Actors.ToListAsync();
			return result;
		}

		public async Task<Actor> UpdateAsync(int id, Actor newActor)
		{
			_context.Update(newActor);
			await _context.SaveChangesAsync();
			return newActor;
		}
	}
}
