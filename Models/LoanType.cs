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
        public string LoanTypeName { get; set; }
        public string Duration { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
