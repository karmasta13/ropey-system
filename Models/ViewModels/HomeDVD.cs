using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models.ViewModels
{
    public class HomeDVD
    {
        public string? DVDTitleName { get; set; }

        public string? DVDPictureURL { get; set; }

        public string? DVDCategory { get; set; }

        public string? CastMember { get; set; }

        public decimal StandardCharge { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateReleased { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
