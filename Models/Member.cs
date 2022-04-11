using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RopeyDVDSystem.Models
{
    public class Member
    {
        public int MemberNumber { get; set; }
        public int MemberCategoryNumber { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string MemberAddress { get; set; }
        public DateOnly MemberDateOfBirth { get; set; }

        public MembershipCategory MembershipCategory { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
