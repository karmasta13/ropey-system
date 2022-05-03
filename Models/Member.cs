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
        public int  MemberCategoryNumber { get; set; }

        [Display(Name = "First Name")]
        public string MemberFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string MemberLastName { get; set; }

        [Display(Name = "Address")]
        public string MemberAddress { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime MemberDateOfBirth { get; set; }

        //relationships
        public virtual MembershipCategory MembershipCategory { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
