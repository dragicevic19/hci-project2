using System;
using System.Collections.Generic;
using System.Text;
using TrainTickets.model.departure;

namespace TrainTickets.dto
{
    public class DepartureDTO
    {
        public int DepartureId { get; set; }

        public int RouteId { get; set; }

        public string StartStation { get; set; }

        public string EndStation { get; set; }

        public string date { get; set; }

        public string startTime { get; set; }

        public DepartureDTO()
        {
        }
        
        public DepartureDTO(Departure departure)
        {
            DepartureId = departure.Id;
            RouteId = departure.RouteId;
            StartStation = departure.Route.Stations[0].Station.Name;
            EndStation = departure.Route.Stations[^1].Station.Name;
            date = departure.DepartureTime.Date.ToString("dd/MM/yyyy");
            DateTime datetime = departure.DepartureTime;
            startTime = datetime.TimeOfDay.ToString(@"hh\:mm");
        }
    }
}
