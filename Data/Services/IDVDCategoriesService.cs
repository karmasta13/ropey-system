using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.Services
{
    public interface IDVDCategoriesService
    {
        Task<IEnumerable<DVDCategory>> GetAllAsync();
        Task<DVDCategory> GetDVDCategoryAsync(int id);
        Task AddAsync(DVDCategory dVDCategory);
        Task<DVDCategory> UpdateAsync(int id, DVDCategory newDVDCategory);
        Task DeleteAsync(int id);
    }
}
