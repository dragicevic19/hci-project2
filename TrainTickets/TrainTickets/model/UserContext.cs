using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
