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
        public string MembershipCategoryDescription { get; set; }
        public int MembershipCategoryTotalLoans { get; set; }

        //relationship
        public ICollection<Member> Members { get; set; }
    }
}
