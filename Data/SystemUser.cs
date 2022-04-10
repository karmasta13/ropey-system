using Microsoft.AspNetCore.Identity;

namespace RopeyDVDSystem.Data
{
    public class SystemUser : IdentityUser 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserType { get; set; }
    }
}
