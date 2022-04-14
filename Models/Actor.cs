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
        public string ActorPictureURL { get; set; }
        [Required]
        public string ActorFirstName { get; set; }
        public string ActorSurname { get; set; }

        //Relationship
        public ICollection<CastMember> CastMembers { get; set; }
    }
}
