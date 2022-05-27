using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.train
{
    public class TrainContext : DbContext
    {
        public DbSet<Train> Trains { get; set; }

    }
}
