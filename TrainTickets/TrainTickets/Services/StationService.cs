using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.Database;
 
using TrainTickets.model.station;

namespace TrainTickets.Services
{
    public class StationService
    {

        public List<Station> AllStations()
        {

            List<Station> stations;

            using (var db = new DatabaseContext())
            {
               stations = db.Stations.ToList();

            }

            return stations;

        }
    }
}   
