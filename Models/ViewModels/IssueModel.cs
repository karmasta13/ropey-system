using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models.ViewModels
{
    public class IssueModel
    {
        public string? DVDTitleName { get; set; }

        public string? DVDCategory { get; set; }

        public int CopyNumber { get; set; }

        [Display(Name = "Release Date")]
        public DateTime DateReleased { get; set; }

        public string AgeRestricted { get; set; }

        public int LoanTypeNumber { get; set; }

        public int MemberNumber { get; set; }
    }
}
