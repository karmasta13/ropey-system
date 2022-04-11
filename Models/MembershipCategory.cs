using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RopeyDVDSystem.Models
{
    public class MembershipCategory
    {
        public int MembershipCategoryNumber { get; set; }
        public string MembershipCategoryDescription { get; set; }
        public string MembershipCategoryTotalLoans { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
