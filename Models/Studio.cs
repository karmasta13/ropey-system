using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RopeyDVDSystem.Models
{
    public class Studio
    {
        [Key]
        public int StudioNumber { get; set; }

        [Display(Name = "Studio Name")]
        public string StudioName { get; set; }

        //relationship
        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
