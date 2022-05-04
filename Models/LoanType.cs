using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RopeyDVDSystem.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeNumber { get; set; }

        [Display(Name = "Loan Type Name")]
        public string LoanTypeName { get; set; }

        [Display(Name = "Duration")]
        public int Duration { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
