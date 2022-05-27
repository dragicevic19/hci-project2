using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.train;
using TrainTickets.model.trainRoute;

namespace TrainTickets.model.departure
{
    public class Departure
    {
        [Key]
        public int Id { get; set; }

        public TrainRoute Route { get; set; } 

        public Train Train { get; set; }

        public DateTime DepartureTime { get; set; }
    }
}
