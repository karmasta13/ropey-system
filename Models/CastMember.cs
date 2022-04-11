using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models
{
    public class CastMember
    {
        [Required]
        public int ActorNumber { get; set; }
        [Required]
        public int DVDNumber { get; set; }

        public Actor Actor { get; set; }
        public DVDTitle DVDTitle { get; set; }
    }
}
