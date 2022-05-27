using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.trainSeats
{
    public class TrainSeatsContext : DbContext
    {
        public DbSet<TrainSeats> TrainSeats { get; set; }
    }
}
