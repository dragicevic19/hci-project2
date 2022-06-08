using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.trainSeats;

namespace TrainTickets.model.train
{
    public class Train
    {
        [Key]
        public int Id { get; set; }

        public int Capacity { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        //public virtual List<TrainSeats> Seats { get; set; }

    }
}
