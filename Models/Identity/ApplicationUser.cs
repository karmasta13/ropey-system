using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models.Identity
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "Full name")]
        public string? FullName { get; set; }
      
    }
}
