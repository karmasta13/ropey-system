using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models
{
    public class DVDCategory
    {
        [Required]
        public int CategoryNumber { get; set; }
        public string CategoryDescription { get; set; }
        public string AgeRestricted { get; set; }

        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
