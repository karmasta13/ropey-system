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
        public int LoanTypeNumber { get; set; }
        [ForeignKey("CopyNumber")]
        public int CopyNumber { get; set; }
        [ForeignKey("MemberNumber")]
        public int MemberNumber { get; set; }
        public DateOnly DateOut { get; set; }
        public DateOnly DateDue { get; set; }
        public DateOnly DateReturn { get; set; }

        //relationships
        public LoanType LoanType { get; set; }
        public DVDCopy DVDCopy { get; set; }
        public Member Member { get; set; }
    }
}
