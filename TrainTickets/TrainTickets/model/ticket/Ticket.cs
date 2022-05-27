using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.stationOnRoute;

namespace TrainTickets.model.ticket
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public StationOnRoute StartStation { get; set; }

        public StationOnRoute EndStation { get; set; }

        public double Price { get; set; }

        public bool IsPurchased { get; set; }

        public DateTime PurchaseDateTime { get; set; }
    }
}
