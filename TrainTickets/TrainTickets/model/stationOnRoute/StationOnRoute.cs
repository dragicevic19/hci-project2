using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.station;

namespace TrainTickets.model.stationOnRoute
{
    public class StationOnRoute
    {

        [Key]
        public int Id { get; set; }

        public Station Station { get; set; }

        public DateTime DateTime { get; set; }

    }
}
