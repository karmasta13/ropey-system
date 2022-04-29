using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RopeyDVDSystem.Models
{
    public class DVDCopy
    {
        [Key]
        public int CopyNumber { get; set; }
        [ForeignKey("DVDNumber")]
        public int DVDNumber { get; set; }
        public DateTime DatePurchased { get; set; }

        //Relationship
        public virtual DVDTitle DVDTitle { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
