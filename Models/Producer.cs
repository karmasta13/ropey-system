using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RopeyDVDSystem.Models
{
    public class Producer
    {
        [Key]
        public int ProducerNumber { get; set; }

        [Display(Name = "Producer Picture")]
        public string ProducerPictureURL { get; set; }

        [Display(Name = "Name")]
        public string ProducerName { get; set; }


        //relationship
        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
