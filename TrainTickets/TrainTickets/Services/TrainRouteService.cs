using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TrainTickets.Database;
using TrainTickets.dto;
using TrainTickets.model.stationOnRoute;
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

        internal bool addRoute(ObservableCollection<StationOnRouteDTO> selectedStations, string name, List<DepartureTime> departureTimes)
        {
            if (FindByName(name) != null)
            {
                return false;
            }

            TrainRoute newRoute = new TrainRoute();

            foreach(var s in selectedStations)
            {
                newRoute.Stations.Add(new StationOnRoute(s.Station, newRoute, s.AdditionalTime, s.AdditionalPrice));
            }
            foreach(var d in departureTimes)
            {
                newRoute.DepartureTimes.Add(d);
            }
            newRoute.Name = name;

            return save(newRoute);

        }

        public bool save(TrainRoute newRoute)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    db.TrainRoutes.Add(newRoute);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
