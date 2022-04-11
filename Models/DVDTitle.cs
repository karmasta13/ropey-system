using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RopeyDVDSystem.Models
{
    public class DVDTitle
    {
        public int DVDNumber { get; set; }
        public int CategoryNumber { get; set; }
        public int StudioNumber { get; set; }
        public int ProducerNumber { get; set; }
        public string DVDTitleName { get; set; }
        public DateOnly DateReleased { get; set; }
        public decimal StandardCharge { get; set; }
        public decimal PenaltyCharge { get; set; }

        public DVDCategory DVDCategory { get; set; }
        public Studio Studio { get; set; }
        public Producer Producer { get; set; }

        public ICollection<CastMember> CastMembers { get; set; }
        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
