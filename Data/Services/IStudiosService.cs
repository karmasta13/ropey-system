using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.Services
{
    public interface IStudiosService
    {
        Task<IEnumerable<Studio>> GetAllAsync();
        Task<Studio> GetStudioAsync(int id);
        Task AddAsync(Studio studio);
        Task<Studio> UpdateAsync(int id, Studio newStudio);
        Task DeleteAsync(int id);
    }
}
