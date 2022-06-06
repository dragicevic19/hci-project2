using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTickets.model.station
{
    public class Station
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public Location Location { get; set; }
 
        public Station(int id, string name, Location location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
        public Station() { }

 
        public override string ToString()
 
        {
            return Name;
        }
 
    }
}
