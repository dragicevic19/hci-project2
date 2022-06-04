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
            var db = new DatabaseContext();
            List<Station> stations = db.Stations.ToList();

            return stations;

        }
    }
}   
