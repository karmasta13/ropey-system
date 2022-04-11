using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RopeyDVDSystem.Models
{
    public class Loan
    {
        public int LoanNumber { get; set; }
        public int LoanTypeNumber { get; set; }
        public int CopyNumber { get; set; }
        public int MemberNumber { get; set; }
        public DateOnly DateOut { get; set; }
        public DateOnly DateDue { get; set; }
        public DateOnly DateReturn { get; set; }

        public LoanType LoanType { get; set; }
        public DVDCopy DVDCopy { get; set; }
        public Member Member { get; set; }
    }
}
