using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.ticket
{
    public class TicketContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
    }
}
