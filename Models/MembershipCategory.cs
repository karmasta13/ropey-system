using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RopeyDVDSystem.Models
{
    public class MembershipCategory
    {
        [Key]
        public int MembershipCategoryNumber { get; set; }

        [Display(Name = "Membership Category Name")]
        public string MembershipCategoryName { get; set; }

        [Display(Name = "Membership Category Description")]
        public string MembershipCategoryDescription { get; set; }

        [Display(Name = "Total Loans")]
        public int MembershipCategoryTotalLoans { get; set; }


        //relationship
        public ICollection<Member> Members { get; set; }
    }
}
