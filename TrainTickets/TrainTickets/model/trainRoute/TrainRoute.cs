using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.stationOnRoute;
using TrainTickets.model.train;

namespace TrainTickets.model.trainRoute
{
    public class TrainRoute
    {
        [Key]
        public int Id { get; set; } 

        public string Name { get; set; }

        public virtual List<StationOnRoute> Stations { get; set; }

        public virtual List<DepartureTime> DepartureTimes { get; set; }

        public bool Deleted { get; set; }

        internal bool ContainsStation(string stationName)
        {
            foreach(var station in Stations)
            {
                if (station.Station.Name.ToLower().Contains(stationName))
                    return true;
            }
            return false;
        }

        public TrainRoute()
        {
            this.Stations = new List<StationOnRoute>();
            this.DepartureTimes = new List<DepartureTime>();
            this.Deleted = false;
        }

        // public double Price { get; set; } racunamo kao zbir additionalPrices u StationOnRoute
    }
}
