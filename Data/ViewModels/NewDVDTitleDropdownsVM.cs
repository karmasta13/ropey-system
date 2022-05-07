using RopeyDVDSystem.Models;

namespace RopeyDVDSystem.Data.ViewModels
{
    public class NewDVDTitleDropdownsVM
    {
        public NewDVDTitleDropdownsVM()
        {
            Studios = new List<Studio>();
            DVDCategories = new List<DVDCategory>();
            Producers = new List<Producer>();
            Actors = new List<Actor>();
        }

        public List<Studio> Studios { get; set; }
        public List<DVDCategory> DVDCategories { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
