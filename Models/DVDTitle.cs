using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RopeyDVDSystem.Models
{
    public class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }

        [ForeignKey("CategoryNumber")]
        public int CategoryNumber { get; set; }

        [ForeignKey("StudioNumber")]
        public int StudioNumber { get; set; }

        [ForeignKey("ProducerNumber")]
        public int ProducerNumber { get; set; }

        public string DVDPictureURL { get; set; }
        public string DVDTitleName { get; set; }
        public DateOnly DateReleased { get; set; }
        public decimal StandardCharge { get; set; }
        public decimal PenaltyCharge { get; set; }

        //relationships
        public DVDCategory DVDCategory { get; set; }
        public Studio Studio { get; set; }
        public Producer Producer { get; set; }

        public ICollection<CastMember> CastMembers { get; set; }
        public ICollection<DVDCopy> DVDCopies { get; set; }
    }
}
