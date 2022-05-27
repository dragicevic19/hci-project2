using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.stationOnRoute
{
    public class StationOnRouteContext : DbContext
    {
        public DbSet<StationOnRoute> StationsOnRoutes { get; set; }

    }
}
