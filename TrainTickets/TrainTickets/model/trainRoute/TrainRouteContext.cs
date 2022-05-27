using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.trainRoute
{
    public class TrainRouteContext : DbContext
    {
        public DbSet<TrainRoute> TrainRoutes { get; set; } 
    }
}
