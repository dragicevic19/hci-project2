using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.station
{
    public class StationContext : DbContext
    {
        public DbSet<Station> Stations { get; set; }
    }
}
