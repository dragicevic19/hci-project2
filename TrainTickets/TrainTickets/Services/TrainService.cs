using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainTickets.Database;
using TrainTickets.model.train;

namespace TrainTickets.Services
{
    public class TrainService
    {

        public List<Train> getAll()
        {
            using(var db = new DatabaseContext())
            {
                return db.Trains.ToList();
            }
        }

        public Train findByName(string trainName)
        {
            if (trainName == null) return null;

            using (var db = new DatabaseContext())
            {
                foreach (var train in db.Trains)
                {
                    if (trainName == train.Name && !train.Deleted)
                    {
                        return train;
                    }
                }
            }
            return null;
        }
    }
}
