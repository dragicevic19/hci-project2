using System;
using System.Collections.Generic;
using System.Text;
using TrainTickets.Database;
using TrainTickets.dto;
using TrainTickets.model;
using TrainTickets.model.departure;
using TrainTickets.model.station;
using TrainTickets.model.ticket;

namespace TrainTickets.Services
{
    internal class TicketService
    {


        public bool createTicket(RoutesForViewWithPriceDTO rfv, User user, int departureID, bool isPurchased)
        {
            try
            {
                using(var db = new DatabaseContext())
                {
                    Departure departure = db.Departures.Find(departureID);
                    if (departure.AvailableSeats == 0) return false;
                    departure.AvailableSeats--;
                    db.SaveChanges();
                }

                Ticket tic = new Ticket();
                tic.UserId = user.Id;
                tic.DepartureID = departureID;
                tic.StartStationId = rfv.start.Id;
                tic.EndStationId = rfv.end.Id;
                tic.Price = rfv.price;
                tic.IsPurchased = isPurchased;
                tic.PurchaseDateTime = DateTime.Now;
                using (var db = new DatabaseContext())
                {
                    db.Tickets.Add(tic);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<Ticket> allUserTickets(User user)
        {
            List<Ticket> ticketList = new List<Ticket>();
            using(var db = new DatabaseContext())
            {
                foreach(var ticket in db.Tickets)
                {
                    if (ticket.UserId == user.Id)
                    {      
                        ticketList.Add(ticket);
                     }
                }
            }
            return ticketList;
        }

        public List<Ticket> allTicketsForDeparture(int departureId)
        {
            List<Ticket> retList = new List<Ticket>();

            using (var db = new DatabaseContext())
            {
                foreach(var ticket in db.Tickets)
                {
                    if (ticket.DepartureID == departureId && ticket.IsPurchased)
                    {
                        retList.Add(ticket);
                    }
                }
            }
            return retList;
        }

        public List<Ticket> allTicketsMon(Station start, Station end, DateTime datumpoc)
        {
            DateTime mesecdana = datumpoc.AddMonths(1);

            
            List<Ticket> ticketList = new List<Ticket>();
            using (var db = new DatabaseContext())
            {
                foreach (var ticket in db.Tickets)
                {
                    if (datumpoc < ticket.PurchaseDateTime && ticket.PurchaseDateTime < mesecdana)
                    {
                        bool d = false;
                        if (start == null && end == null)
                            d = true;

                        if (start == null && end != null)
                            if (end.Name.Equals(db.Stations.Find(ticket.EndStationId).Name))
                                d = true;
                        if (start != null && end == null)
                            if (start.Name.Equals(db.Stations.Find(ticket.StartStationId).Name))
                                d = true;

                        if (start != null && end != null)
                            if (end.Name.Equals(db.Stations.Find(ticket.EndStationId).Name))
                                if (start.Name.Equals(db.Stations.Find(ticket.StartStationId).Name))
                                    d = true;
                    
                        if(d && ticket.IsPurchased)
                            ticketList.Add(ticket);
                    }
                   
                }
            }
            return ticketList;
        }


        public List<Ticket> allReservationUserTickets(User user)
        {
            List<Ticket> ticketList = new List<Ticket>();
            using (var db = new DatabaseContext())
            {
                foreach (var ticket in db.Tickets)
                {
                    if (ticket.UserId == user.Id && !ticket.IsPurchased)
                    {
                        ticketList.Add(ticket);
                        db.SaveChanges();
                    }
                }
            }
            return ticketList;
        }

        public List<Ticket> allPurchasedUserTickets(User user)
        {
            List<Ticket> ticketList = new List<Ticket>();
            using (var db = new DatabaseContext())
            {
                foreach (var ticket in db.Tickets)
                {
                    if (ticket.UserId == user.Id && ticket.IsPurchased)
                        ticketList.Add(ticket);

                }
            }
            return ticketList;
        }

        public List<TicketViewDTO> listToDTOList(List<Ticket> tickets)
        {
            List<TicketViewDTO> list = new List<TicketViewDTO>();
            using (var db = new DatabaseContext()) {
                foreach (var t in tickets)
                {
                    Departure d = db.Departures.Find(t.DepartureID);
                    Station start=db.Stations.Find(t.StartStationId);
                    Station end = db.Stations.Find(t.EndStationId);
                    double price = t.Price;
                    String date = d.DepartureTime.Date.ToString("dd/MM/yyyy");
                    DateTime datetime = d.DepartureTime;
                    TimeSpan tp = datetime.TimeOfDay;
                    String startTime = (tp + TimeSpan.FromMinutes(d.Route.sumAddTime(start))).ToString(@"hh\:mm");
                    String timeshopping = t.PurchaseDateTime.ToString("dd/MM/yyyy hh:mm");
                    TicketViewDTO tv = new TicketViewDTO(start, end, price, date, startTime, t,timeshopping, datetime);
                    tv.mail = db.Users.Find(t.UserId).Email;
                    tv.kupljena = t.IsPurchased;
                    list.Add(tv);
                 } 
            }
            return list;
        }

        public bool purished(int id)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    db.Tickets.Find(id).IsPurchased = true;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}
