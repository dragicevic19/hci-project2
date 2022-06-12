using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TrainTickets.Database;
using TrainTickets.dto;
using TrainTickets.model.station;
using TrainTickets.model.stationOnRoute;
using TrainTickets.model.train;
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

        public bool addRoute(ObservableCollection<StationOnRouteDTO> selectedStations, string name, List<DepartureTime> departureTimes, Train train)
        {
            if (FindByName(name) != null)
            {
                return false;
            }

            TrainRoute newRoute = new TrainRoute();

            foreach(var s in selectedStations)
            {
                Station st = findStationByName(s.Station.Name);
                newRoute.Stations.Add(new StationOnRoute(st.Id, newRoute, s.AdditionalTime, s.AdditionalPrice));
            }
            foreach(var d in departureTimes)
            {
                newRoute.DepartureTimes.Add(d);
            }

            newRoute.TrainId = train.Id;
            newRoute.Name = name;

            return save(newRoute);

        }

        private Station findStationByName(string name)
        {
            using (var db = new DatabaseContext())
            {
                foreach(var station in db.Stations)
                {
                    if (station.Name == name)
                    {
                        return station;
                    }
                }
            }
            return null;
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
                        retList.Add(new TrainRouteDTO(route.Name, route.Stations[0].Station.Name, route.Stations[^1].Station.Name, route.DepartureTimes, route.Train.Name));
                    }
                }
            }
            return retList;
        }

        internal bool editRoute(ObservableCollection<StationOnRouteDTO> selectedStations, string name, List<DepartureTime> departureTimes)
        {
            try
            {
                TrainRoute route = FindByName(name);
                if (route == null) return false;

                using (var db = new DatabaseContext())
                {
                    IList<TrainRoute> routes = (from tr in db.TrainRoutes select tr).ToList();

                    foreach (var r in routes)
                    {
                        if (r.Id == route.Id)
                        {
                            db.StationOnRoutes.RemoveRange(r.Stations);
                            r.Stations.Clear();

                            foreach (var s in selectedStations)
                            {
                                Station st = findStationByName(s.Station.Name);
                                r.Stations.Add(new StationOnRoute(st.Id, r.Id, s.AdditionalTime, s.AdditionalPrice));
                            }
                            db.DepartureTimes.RemoveRange(r.DepartureTimes);
                            r.DepartureTimes.Clear();
                            foreach(var dt in departureTimes)
                            {
                                r.DepartureTimes.Add(dt);
                            }
                            db.SaveChanges();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CanEditOrDelete(TrainRouteDTO row)
        {
            TrainRoute route = FindByName(row.Name);
            if (route == null) return false;

            using(var db = new DatabaseContext())
            {
                foreach(var dep in db.Departures)
                {
                    if (dep.Route.Id == route.Id && dep.DepartureTime > DateTime.Now) return false;
                }
            }
            return true;
        }
    }
}
