
using RopeyDVDSystem.Models;
using RopeyDVDSystem.Models.ViewModels;

namespace RopeyDVDSystem.Data.Services
{
    public interface IMembersService
    {

        Task<IEnumerable<MemberDetailViewModel>> GetAllDetailsAsync();
        Task<IEnumerable<MemberDetailViewModel>> GetAllDetailsAsync(int id, string lastName);
     

    }
}
