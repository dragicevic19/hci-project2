using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.station;

namespace TrainTickets.model.stationOnRoute
{
    public class StationOnRoute
    {

        [Key]
        public int Id { get; set; }

        public virtual Station Station { get; set; }

        [ForeignKey("Station")]
        public int StationId { get; set; }

        public DateTime DateTime { get; set; }

        public double AdditionalTime { get; set; }
        public double AdditionalPrice { get; set; }

    }
}
