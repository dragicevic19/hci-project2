using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTickets.model.stationOnRoute;
using TrainTickets.model.train;
using TrainTickets.model.station;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainTickets.model.trainRoute
{
    public class TrainRoute
    {
        [Key]
        public int Id { get; set; } 

        public string Name { get; set; }

        public virtual List<StationOnRoute> Stations { get; set; }

        public virtual List<DepartureTime> DepartureTimes { get; set; }

        [ForeignKey("Train")]
        public int TrainId { get; set; }
        public virtual Train Train { get; set; }

        public bool Deleted { get; set; }



        internal bool ContainsStation(string stationName)
        {
            foreach(var station in Stations)
            {
                if (station.Station.Name.ToLower().Contains(stationName))
                    return true;
            }
            return false;
        }

        public TrainRoute()
        {
            this.Stations = new List<StationOnRoute>();
            this.DepartureTimes = new List<DepartureTime>();
            this.Deleted = false;
        }

        public string depTimesToStr()
        {
            string retStr = "";

            foreach (var time in DepartureTimes)
            {
                retStr += time.Time.Hours + ":" + time.Time.Minutes + "; ";
            }
            return retStr;
        }


        public bool containsRoute(Station start, Station end)
        {
            if (start == null || end == null || start.Equals(end))
                return false;
            bool firstFound = false;
            foreach (var t in this.Stations)
            {
                 
                if (t.Station.Name.Equals(start.Name))
                    firstFound = true;
                else
                {
                    if (t.Station.Name.Equals(end.Name))
                    {
                        if (firstFound)
                            return true;
                        return false;
                    }
                } 
            }
            return false;

        }

        public double routePrice(Station start, Station end)
        {
            bool firstFound = false;
            double price = 0;
            foreach(var t in this.Stations)
            {
                if (t.Station.Name.Equals(start.Name))
                {
                    firstFound = true;
                    continue;
                }
                else if (t.Station.Name.Equals(end.Name) && firstFound)
                {
                    price += t.AdditionalPrice;
                    break;
                }
                else
                    price += t.AdditionalPrice;

            }
            return price;
        }
        public double sumAddTime(Station s)
        {
            return this.routeTime(Stations[0].Station, s);
        }

      

        public double routeTime(Station start, Station end)
        {
            bool firstFound = false;
        
            double time = 0;
            double firstAddTime = 0;
            foreach (var t in this.Stations)
            {
                if (t.Station.Name.Equals(start.Name))
                {
                    firstFound = true;
                    firstAddTime = t.AdditionalTime;
                   
                }
               if (t.Station.Name.Equals(end.Name))
                {

                    time += t.AdditionalTime;
                    break;
                }
                if(firstFound == true)
                    time += t.AdditionalTime;

            }
            return time- firstAddTime;
        }

        public override bool Equals(object obj)
        {
            return obj is TrainRoute route &&
                   Id == route.Id;
        }
    }
}
