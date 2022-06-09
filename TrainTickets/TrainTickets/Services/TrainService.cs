using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TrainTickets.Database;
using TrainTickets.dto;
using TrainTickets.model.train;
using TrainTickets.model.trainSeats;
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
            List<Train> list = new List<Train>();
            using(var db = new DatabaseContext())
            {
                foreach(var t in db.Trains)
                {
                    if (!t.Deleted)
                    {
                        list.Add(t);
                    }
                }
            }
            return list;
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

        public Train FindById(int id)
        {
            using (var db = new DatabaseContext())
            {
                foreach (var train in db.Trains)
                {
                    if (id == train.Id && !train.Deleted)
                    {
                        return train;
                    }
                }
            }
            return null;
        }

        public bool save(Train newTrain)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    db.Trains.Add(newTrain);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ObservableCollection<TrainDTO> allTrainsToDTO()
        {
            ObservableCollection<TrainDTO> retList = new ObservableCollection<TrainDTO>();

            using (var db = new DatabaseContext())
            {
                foreach (var train in db.Trains)
                {
                    if (!train.Deleted)
                    {
                        retList.Add(new TrainDTO(train.Name, train.Capacity));
                    }
                      
                }
            }
            return retList;
        }

        public bool deleteTrain(TrainDTO row)
        {
            try
            {
                Train train = findByName(row.Name);
                if (train == null) return false;

                using (var db = new DatabaseContext())
                {
                    IList<Train> trains = (from tr in db.Trains select tr).ToList();

                    foreach (var t in trains)
                    {
                        if (t.Id == train.Id)
                        {
                            t.Deleted = true;
                            db.SaveChanges();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal bool editTrain(string name, string capacity)
        {
            try
            {
                Train train = findByName(name);
                if (train == null) return false;

                using (var db = new DatabaseContext())
                {
                    IList<Train> trains = (from t in db.Trains select t).ToList();

                    foreach (var t in trains)
                    {
                        if (t.Id == train.Id)
                        {
                            t.Capacity = Int32.Parse(capacity);
                            db.SaveChanges();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool addTrain(string name, string capacity)
        {
            if (findByName(name) != null)
            {
                return false;
            }

            Train newTrain = new Train();

            newTrain.Name = name;
            newTrain.Capacity = Int32.Parse(capacity);

            return save(newTrain);
        }

        public bool CanEditOrDeleteTrain(TrainDTO row)
        {
            Train train = findByName(row.Name);

            using(var db = new DatabaseContext())
            {
                foreach(var route in db.TrainRoutes)
                {
                    if (route.Deleted) continue;
                    if (route.Train.Id == train.Id) return false;
                }
            }
            return true;
        }
    }
}
