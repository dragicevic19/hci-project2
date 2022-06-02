using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public virtual TrainRoute Route { get; set; }

        [ForeignKey("Route")]
        public int RouteId { get; set; }

        public virtual Train Train { get; set; }

        [ForeignKey("Train")]
        public int TrainId { get; set; }

        public DateTime DepartureTime { get; set; }
    }
}
