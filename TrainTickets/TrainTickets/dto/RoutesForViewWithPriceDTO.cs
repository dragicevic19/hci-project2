using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.station;
using TrainTickets.model.trainRoute;

namespace TrainTickets.dto
{
    internal class RoutesForViewWithPriceDTO
    {
        

        TrainRoute Tr { get; set; }
        Station start { get; set; }

        Station end { get; set; }

        double price { get; set; }

        TimeOnly startTime { get; set; }

        double time { get; set; }

        public RoutesForViewWithPriceDTO()
        {
        }

        public RoutesForViewWithPriceDTO(TrainRoute tr, Station start, Station end, double price, TimeOnly startTime, double time)
        {
            Tr = tr;
            this.start = start;
            this.end = end;
            this.price = price;
            this.startTime = startTime;
            this.time = time;
        }
    }
}
