﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.departure
{
    public class DepartureContext : DbContext
    {
        public DbSet<Departure> Departures { get; set; }
    }
}
