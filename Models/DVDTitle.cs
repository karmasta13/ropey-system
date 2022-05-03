using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RopeyDVDSystem.Models
{
    public  class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }

        [Display(Name = "Category Number")]
        [ForeignKey("CategoryNumber")]
        public  int CategoryNumber { get; set; }

        [Display(Name = "Studio Number")]
        [ForeignKey("StudioNumber")]
        public  int StudioNumber { get; set; }

        [Display(Name = "Producer Number")]
        [ForeignKey("ProducerNumber")]
        public  int ProducerNumber { get; set; }

        [Display(Name = "DVD Picture")]
        public string DVDPictureURL { get; set; }

        [Display(Name = "DVD Title")]
        public string DVDTitleName { get; set; }

        [Display(Name = "Release Date")]
        public DateTime DateReleased { get; set; }

        [Display(Name = "Standard Charge")]
        public decimal StandardCharge { get; set; }

        [Display(Name = "Penalty Charge")]
        public decimal PenaltyCharge { get; set; }

        //relationships
        public virtual DVDCategory DVDCategory { get; set; }
        public virtual Studio Studio { get; set; }
        public virtual Producer Producer { get; set; }

        public ICollection<CastMember> CastMembers { get; set; }
        public ICollection<DVDCopy> DVDCopies { get; set; }
    }
}
