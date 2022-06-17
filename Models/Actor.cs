using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models
{
    public class Actor
    {
        [Key]
        public int ActorNumber { get; set; }

        [Display(Name = "Actor Picture")]
        [Required(ErrorMessage = "Actor Picture must be entered")]
        public string ActorPictureURL { get; set; }

        [Display(Name = "First Name")]
        [Required (ErrorMessage = "Actor's First Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Actor's first name must be in between 3 and 50 characters")]
        public string ActorFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Actor's Last Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Actor's last name must be in between 3 and 50 characters")]
        public string ActorSurname { get; set; }

    
        public ICollection<CastMember> CastMembers { get; set; }
    }
}
