using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace RopeyDVDSystem.Models
{
    public class Loan
    {
        [Key]
        public int LoanNumber { get; set; }
        [ForeignKey("LoanTypeNumber")]
        public  int LoanTypeNumber { get; set; }

        [Display(Name = "Copy Number")]
        [ForeignKey("CopyNumber")]
        public int CopyNumber { get; set; }
        [ForeignKey("MemberNumber")]
        public  int MemberNumber { get; set; }

        [Display(Name = "Date Out")]
        public DateTime DateOut { get; set; }

        [Display(Name = "Date Due")]
        public DateTime DateDue { get; set; }

        [Display(Name = "Date Return")]
        public DateTime DateReturn { get; set; }

        //relationships
        public virtual LoanType LoanType { get; set; }
        public virtual DVDCopy DVDCopy { get; set; }
        public virtual Member Member { get; set; }
    }
}
