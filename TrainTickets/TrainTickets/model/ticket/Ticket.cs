using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.departure;
using TrainTickets.model.stationOnRoute;
using TrainTickets.model.trainSeats;

namespace TrainTickets.model.ticket
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Departure")]
        public int DepartureID { get; set; }
        public virtual Departure Departure { get; set; }

        public int StartStationId { get; set; }

        public int EndStationId { get; set; }

        public double Price { get; set; }

        public bool IsPurchased { get; set; }

        public DateTime PurchaseDateTime { get; set; }
    }
}
