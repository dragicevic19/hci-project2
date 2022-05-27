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

        public List<StationOnRoute> Stations { get; set; }

        public List<TimeOnly> DepartureTimes { get; set; }

        // public double Price { get; set; } racunamo kao zbir additionalPrices u StationOnRoute
    }
}
