using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models.ViewModels
{
    public class ReturnModel
    {
        public string? DVDTitleName { get; set; }

        public string? DVDCategory { get; set; }

        public int CopyNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateOut { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateDue { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime DateReturn { get; set; }

        public string? MemberName { get; set; }

        public int TotalLoan { get; set; }

        public decimal Payment { get; set; }

        public int LoanNumber { get; set; }

        public int OverDue { get; set; }

        public decimal StandardCharge { get; set; }

        public decimal PenaltyCharge { get; set; }
    }
}

