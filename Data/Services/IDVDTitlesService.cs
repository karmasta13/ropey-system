using eTickets.Data.Base;
using RopeyDVDSystem.Data.ViewModels;
using RopeyDVDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RopeyDVDSystem.Data.Services
{
    public interface IDVDTitlesService:IEntityBaseRepository<DVDTitle>
    {
        Task<DVDTitle> GetDVDTitleByIdAsync(int id);
        Task<NewDVDTitleDropdownsVM> GetNewDVDTitleDropdownsValues();
        Task AddNewDVDTitleAsync(newDVDTitleVM data);
        Task UpdateDVDTitleAsync(newDVDTitleVM data);
    }
}
