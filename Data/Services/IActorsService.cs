using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.Services
{
	public interface IActorsService
	{
		Task<IEnumerable<Actor>> GetAllAsync();
		Task<Actor> GetActorAsync(int id);
		Task AddAsync(Actor actor);
		Task<Actor> UpdateAsync(int id, Actor newActor);
		Task DeleteAsync(int id);
	}
}
