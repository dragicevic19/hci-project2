using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.station;
using TrainTickets.model.trainRoute;

namespace TrainTickets.dto
{
    public class RoutesForViewWithPriceDTO
    {
        

        public TrainRoute Tr { get; set; }
        public Station start { get; set; }

        public Station end { get; set; }

        public double price { get; set; }

        public TimeSpan startTime { get; set; }

        public double time { get; set; }

        public RoutesForViewWithPriceDTO()
        {
        }

        public override string ToString()
        {
            return start + " " + " " +
                end + " " + price + " " + time;
        }

        public RoutesForViewWithPriceDTO(TrainRoute tr, Station start, Station end, double price, TimeSpan startTime, double time)
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
