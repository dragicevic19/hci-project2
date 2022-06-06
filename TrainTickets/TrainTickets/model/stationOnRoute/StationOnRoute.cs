using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.station;
using TrainTickets.model.trainRoute;

namespace TrainTickets.model.stationOnRoute
{
    public class StationOnRoute
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Station")]
        public int StationId { get; set; }
        public virtual Station Station { get; set; }

        [ForeignKey("Route")]
        public int TrainRouteId { get; set; }
        public virtual TrainRoute Route { get; set; }


        //public DateTime DateTime { get; set; } ? 

        public double AdditionalTime { get; set; }
        public double AdditionalPrice { get; set; }


        public StationOnRoute() { }

        public StationOnRoute(Station station)
        {
            Station = station;
        }

        public StationOnRoute(int stationId, int routeId, double time, double price)
        {
            StationId = stationId;
            TrainRouteId = routeId;
            AdditionalTime = time;
            AdditionalPrice = price;
        }

        public StationOnRoute(int stationId, TrainRoute route, double time, double price)
        {
            StationId = stationId;
            Route = route;
            AdditionalTime = time;
            AdditionalPrice = price;
        }
    }
}
