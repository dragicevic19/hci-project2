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
        public Departure()
        {
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Route")]
        public int RouteId { get; set; }
        public virtual TrainRoute Route { get; set; }

        public DateTime DepartureTime { get; set; } // ovo se odnosi na konkretan polazak npr 23.3.2022. 13:00

        public override bool Equals(object obj)
        {
            return obj is Departure departure &&
                   RouteId == departure.RouteId &&
                   DepartureTime.ToString("dd/MM/yyyy hh:mm").Equals(departure.DepartureTime.ToString("dd/MM/yyyy hh:mm"));
        }






        // a 13.00 se vadi iz TrainRoute->DepartureTimes



    }
}
