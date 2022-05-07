using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.Services
{
	public class DVDCategoriesService : IDVDCategoriesService
	{
		private readonly ApplicationDbContext _context;

		public DVDCategoriesService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(DVDCategory dVDCategory)
		{
			await _context.DVDCategories.AddAsync(dVDCategory);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var result = await _context.DVDCategories.FirstOrDefaultAsync(n => n.CategoryNumber == id);
			_context.DVDCategories.Remove(result);
			await _context.SaveChangesAsync();
		}

		public async Task<DVDCategory> GetDVDCategoryAsync(int id)
		{
			var result = await _context.DVDCategories.FirstOrDefaultAsync(n => n.CategoryNumber == id);
			return result;
		}

		public async Task<IEnumerable<DVDCategory>> GetAllAsync()
		{
			var result = await _context.DVDCategories.ToListAsync();
			return result;
		}

		public async Task<DVDCategory> UpdateAsync(int id, DVDCategory newDVDCategory)
		{
			_context.Update(newDVDCategory);
			await _context.SaveChangesAsync();
			return newDVDCategory;
		}
	}
}
