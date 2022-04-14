using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RopeyDVDSystem.Models
{
    public class Member
    {
        [Key]
        public int MemberNumber { get; set; }
        [ForeignKey("MemberCategoryNumber")]
        public int MemberCategoryNumber { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string MemberAddress { get; set; }
        public DateOnly MemberDateOfBirth { get; set; }

        //relationships
        public MembershipCategory MembershipCategory { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
