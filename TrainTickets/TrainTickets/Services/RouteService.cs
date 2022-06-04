﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.dto;
using TrainTickets.model.station;
using TrainTickets.Database;

namespace TrainTickets.Services
{
    internal class RouteService
    {
        public List<RoutesForViewWithPriceDTO> routesWithPriceAndTime(Station start, Station end)//vraca sve polaske ovde moze da se izbaci iz dto-a  timeonly departure times pa samim tim i ceo ovaj for drugi
        {
            List<RoutesForViewWithPriceDTO> routes = new List<RoutesForViewWithPriceDTO>(); 
            using (var db = new DatabaseContext())
            {
                foreach (var route in db.TrainRoutes)
                {
                    if(route.containsRoute(start, end))
                    {
                        foreach(var time in route.DepartureTimes)
                        {
                            routes.Add(new RoutesForViewWithPriceDTO(route, start, end, route.routePrice(start, end), time, route.routeTime(start, end)));
                        }    

                    }
                     
                }
            }

            return routes;

        }


    }
}
