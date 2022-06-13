using System;
using System.Collections.Generic;
using System.Text;
using TrainTickets.Database;
using TrainTickets.dto;
using TrainTickets.model.departure;
using TrainTickets.model.station;
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

        public List<DepartureDTO> allDeparturesToDTO()
        {
            List<DepartureDTO> retList = new List<DepartureDTO>();
            using (var db = new DatabaseContext())
            {
                foreach (var departure in db.Departures)
                {
                    retList.Add(new DepartureDTO(departure));
                }
            }

            return retList;
        }

        public List<DepartureDTO> departuresThatContainsStations(Station start, Station end)
        {
            List<DepartureDTO> retList = new List<DepartureDTO>();
            using (var db = new DatabaseContext())
            {
                foreach (var departure in db.Departures)
                {
                    bool add = false;
                    if (start == null && end == null)
                        add = true;

                    if (start == null && end != null)
                        if (end.Name.Equals(db.Stations.Find(departure.Route.Stations[^1].StationId).Name))
                            add = true;
                    if (start != null && end == null)
                        if (start.Name.Equals(db.Stations.Find(departure.Route.Stations[0].StationId).Name))
                            add = true;

                    if (start != null && end != null)
                        if (end.Name.Equals(db.Stations.Find(departure.Route.Stations[^1].StationId).Name))
                            if (start.Name.Equals(db.Stations.Find(departure.Route.Stations[0].StationId).Name))
                                add = true;

                    if (add) retList.Add(new DepartureDTO(departure));

                }
            }
            return retList;
        }
    }
}
