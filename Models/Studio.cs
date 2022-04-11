using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RopeyDVDSystem.Models
{
    public class Studio
    {
        public int StudioNumber { get; set; }
        public string StudioName { get; set; }

        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
