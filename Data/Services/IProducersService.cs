using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.Services
{
    public interface IProducersService
    {
        Task<IEnumerable<Producer>> GetAllAsync();
        Task<Producer> GetProducerAsync(int id);
        Task AddAsync(Producer producer);
        Task<Producer> UpdateAsync(int id, Producer newProducer);
        Task DeleteAsync(int id);
    }
}
