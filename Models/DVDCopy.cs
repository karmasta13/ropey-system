using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RopeyDVDSystem.Models
{
    public class DVDCopy
    {

        public int CopyNumber { get; set; }
        public int DVDNumber { get; set; }
        public DateOnly DatePurchased { get; set; }

        public DVDTitle DVDTitle { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
