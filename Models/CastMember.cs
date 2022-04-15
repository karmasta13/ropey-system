﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopeyDVDSystem.Models
{
    public class CastMember
    {
        [ForeignKey("ActorNumber")]
        public int ActorNumber { get; set; }

        [ForeignKey("DVDNumber")]
        public int DVDNumber { get; set; }

        //Relationships
        public Actor Actor { get; set; }
        public DVDTitle DVDTitle { get; set; }
    }
}
