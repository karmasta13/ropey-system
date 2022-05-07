using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.Services
{
	public class StudiosService : IStudiosService
	{
		private readonly ApplicationDbContext _context;

		public StudiosService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Studio studio)
		{
			await _context.Studios.AddAsync(studio);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var result = await _context.Studios.FirstOrDefaultAsync(n => n.StudioNumber == id);
			_context.Studios.Remove(result);
			await _context.SaveChangesAsync();
		}

		public async Task<Studio> GetStudioAsync(int id)
		{
			var result = await _context.Studios.FirstOrDefaultAsync(n => n.StudioNumber == id);
			return result;
		}

		public async Task<IEnumerable<Studio>> GetAllAsync()
		{
			var result = await _context.Studios.ToListAsync();
			return result;
		}

		public async Task<Studio> UpdateAsync(int id, Studio newStudio)
		{
			_context.Update(newStudio);
			await _context.SaveChangesAsync();
			return newStudio;
		}
    }
}
