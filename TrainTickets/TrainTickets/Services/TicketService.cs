using System;
using System.Collections.Generic;
using System.Text;
using TrainTickets.Database;
using TrainTickets.dto;
using TrainTickets.model;
using TrainTickets.model.ticket;

namespace TrainTickets.Services
{
    internal class TicketService
    {


        public bool createTicket(RoutesForViewWithPriceDTO rfv,User user,int departureID,bool isPurchased)
        {
            try
            {
                Ticket tic = new Ticket();
                tic.UserId = user.Id;
                tic.DepartureID = departureID;
                tic.StartStationId = rfv.start.Id;
                tic.EndStationId = rfv.end.Id;
                tic.Price= rfv.price;
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

    }
}
