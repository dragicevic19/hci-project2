using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainTickets.Database;
using TrainTickets.model.trainRoute;

namespace TrainTickets.Services
{
    public class TrainRouteService
    {
        public TrainRoute FindByName(string name)
        {
            if (name == null) return null;

            using (var db = new DatabaseContext())
            {
                foreach (var route in db.TrainRoutes)
                {
                    if (name == route.Name)
                    {
                        return route;
                    }
                }
            }
            return null;
        }
    }
}
