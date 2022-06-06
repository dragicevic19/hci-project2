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
                    if (name == route.Name && !route.Deleted)
                    {
                        return route;
                    }
                }
            }
            return null;
        }

        public bool addRoute(ObservableCollection<StationOnRouteDTO> selectedStations, string name, List<DepartureTime> departureTimes)
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

        public bool deleteRoute(TrainRouteDTO row)
        {
            try
            {
                TrainRoute route = FindByName(row.Name);
                if (route == null) return false;

                using (var db = new DatabaseContext())
                {
                    IList<TrainRoute> routes = (from tr in db.TrainRoutes select tr).ToList();

                    foreach(var r in routes)
                    {
                        if (r.Id == route.Id)
                        {
                            r.Deleted = true;
                            db.SaveChanges();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public ObservableCollection<TrainRouteDTO> allRoutesToDTO()
        {
            ObservableCollection<TrainRouteDTO> retList = new ObservableCollection<TrainRouteDTO>();

            using(var db = new DatabaseContext())
            {
                foreach(var route in db.TrainRoutes)
                {
                    if (!route.Deleted)
                    {
                        retList.Add(new TrainRouteDTO(route.Name, route.Stations[0].Station.Name, route.Stations[^1].Station.Name, route.DepartureTimes));
                    }
                }
            }

            return retList;
        }
    }
}
