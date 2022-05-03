using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RopeyDVDSystem.Models
{
    public class DVDCategory
    {
        [Key]
        public int CategoryNumber { get; set; }

		[Display(Name = "Category Name")]
		public string CategoryName { get; set; }

		[Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        [Display(Name = "Restricted Age")]
        public string AgeRestricted { get; set; }

        //Relationship
        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
