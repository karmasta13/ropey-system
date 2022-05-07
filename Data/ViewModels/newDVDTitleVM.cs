using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTickets.Data.Base;

namespace RopeyDVDSystem.Models
{
    public  class newDVDTitleVM
    {
        public int DVDNumber { get; set; }

        [Display(Name = "Select a category")]
        [ForeignKey("CategoryNumber")]
        public  int CategoryNumber { get; set; }

        [Display(Name = "Select a studio")]
        [ForeignKey("StudioNumber")]
        public  int StudioNumber { get; set; }

        [Display(Name = "Select a Producer")]
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

        [Display(Name = "Select actor(s)")]
        public ICollection<int> ActorNumbers { get; set; }
        public ICollection<DVDCopy> DVDCopies { get; set; }
    }
}
