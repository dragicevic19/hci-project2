using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model;
using TrainTickets.model.departure;
using TrainTickets.model.station;
using TrainTickets.model.stationOnRoute;
using TrainTickets.model.ticket;
using TrainTickets.model.train;
using TrainTickets.model.trainRoute;
using TrainTickets.model.trainSeats;

namespace TrainTickets.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<TrainSeats> TrainSeats { get; set; }

        public DbSet<OneSeat> OneSeats { get; set; }

        public DbSet<TrainRoute> TrainRoutes { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<StationOnRoute> StationOnRoutes { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<Departure> Departures { get; set; }

        public DbSet<DepartureTime> DepartureTimes { get; set; }
    }
}
