using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.trainSeats
{

    public class OneSeat
    {
        [Key]
        public int Id { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

    }
}
