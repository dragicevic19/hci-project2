using System;
using System.Collections.Generic;
using System.Text;
using TrainTickets.model.trainRoute;

namespace TrainTickets.dto
{
    public class TrainRouteDTO
    {
        public string Name { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string DepartureTimes { get; set; }

        public string TrainName { get; set; }

        public TrainRouteDTO() { }

        public TrainRouteDTO(string name, string from, string to, List<DepartureTime> departureTimes, string train)
        {
            Name = name;
            From = from;
            To = to;
            DepartureTimes = "";
            foreach(var time in departureTimes)
            {
                DepartureTimes += time.Time.Hours + ":" + time.Time.Minutes + "; ";
            }
            TrainName = train;
        }

        public TrainRouteDTO(string name)
        {
            Name = name;
        }
    }
}
