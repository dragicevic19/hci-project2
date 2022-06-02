using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.station
{
    public class Station
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public Location Location { get; set; }

        //[ForeignKey("Location")]
        //public int LocationId { get; set; }

    }
}
