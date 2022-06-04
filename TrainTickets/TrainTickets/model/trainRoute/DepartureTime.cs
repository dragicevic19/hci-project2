using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrainTickets.model.trainRoute
{
    public class DepartureTime
    {
        [Key]
        public int Id { get; set; }

        public TimeSpan Time { get; set; }

        [ForeignKey("TrainRoute")]
        public int RouteId { get; set; }
        public virtual TrainRoute TrainRoute { get; set; }
    }
}
