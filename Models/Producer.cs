using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RopeyDVDSystem.Models
{
    public class Producer
    {
        public int ProducerNumber { get; set; }
        public string ProducerName { get; set; }

        public ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
