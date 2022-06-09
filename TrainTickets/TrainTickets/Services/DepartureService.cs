using System;
using System.Collections.Generic;
using System.Text;
using TrainTickets.Database;
using TrainTickets.model.departure;
using TrainTickets.model.trainRoute;

namespace TrainTickets.Services
{
    internal class DepartureService
    {


        public Departure findDeparture(TrainRoute tr, DateTime dt)
        {
             
            using (var db = new DatabaseContext())
            {
                foreach (var departure in db.Departures)
                {

                    if (departure.Route.Equals(tr) && departure.DepartureTime.Equals(dt))
                        return departure;
                    

                }
            }
            return null;
        }

        public void createDeparture(TrainRoute tr, DateTime dt)
        {
            Departure d = new Departure();
            d.RouteId = tr.Id;
            d.DepartureTime = dt;
            using (var db = new DatabaseContext())
            {
                db.Departures.Add(d);
                db.SaveChanges();
            }
        }




    }
}
