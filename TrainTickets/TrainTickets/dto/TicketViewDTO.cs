using System;
using System.Collections.Generic;
using System.Text;
using TrainTickets.model.station;
using TrainTickets.model.ticket;

namespace TrainTickets.dto
{
    public  class TicketViewDTO
    {
        public Station start { get; set; }
        public Station end { get; set; }
        public double price { get; set; }
        public String date { get; set; }
        public String startTime { get; set; }
        public Ticket ticket { get; set; }
        public String timeShopping { get; set; }
        public DateTime datetimeofroute { get; set; }

        public TicketViewDTO(Station start, Station end, double price, string date, string startTime, Ticket ticket, string timeShopping, DateTime datetimeofroute)
        {
            this.start = start;
            this.end = end;
            this.price = price;
            this.date = date;
            this.startTime = startTime;
            this.ticket = ticket;
            this.timeShopping = timeShopping;
            this.datetimeofroute = datetimeofroute;
        }
    }
}
