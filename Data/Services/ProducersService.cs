using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.Services
{
	public class ProducersService : IProducersService
	{
		private readonly ApplicationDbContext _context;

		public ProducersService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Producer producer)
		{
			await _context.Producers.AddAsync(producer);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var result = await _context.Producers.FirstOrDefaultAsync(n => n.ProducerNumber == id);
			_context.Producers.Remove(result);
			await _context.SaveChangesAsync();
		}

		public async Task<Producer> GetProducerAsync(int id)
		{
			var result = await _context.Producers.FirstOrDefaultAsync(n => n.ProducerNumber == id);
			return result;
		}

		public async Task<IEnumerable<Producer>> GetAllAsync()
		{
			var result = await _context.Producers.ToListAsync();
			return result;
		}

		public async Task<Producer> UpdateAsync(int id, Producer newProducer)
		{
			_context.Update(newProducer);
			await _context.SaveChangesAsync();
			return newProducer;
		}
	}
}
