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
        [Required(ErrorMessage = "Category name must be entered")]
        public string CategoryName { get; set; }

		[Display(Name = "Category Description")]
        [Required(ErrorMessage = "Category description must be entered")]
        public string CategoryDescription { get; set; }

        [Display(Name = "Restricted Age")]
        [Required(ErrorMessage = "Age Restricted must be entered")]
        public string AgeRestricted { get; set; }

        //Relationship
        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
