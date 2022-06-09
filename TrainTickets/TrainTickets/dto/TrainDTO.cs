using System;
using System.Collections.Generic;
using System.Text;

namespace TrainTickets.dto
{
    public class TrainDTO
    {
        public string Name { get; set; }

        public int Capacity { get; set; }

        public TrainDTO() { }

        public TrainDTO(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }
    }
}
