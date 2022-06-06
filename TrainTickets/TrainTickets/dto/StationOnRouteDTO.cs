using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using TrainTickets.model.station;

namespace TrainTickets.dto
{
    public class StationOnRouteDTO : INotifyPropertyChanged
    {

        private Station _station;
        public Station Station
        {
            get
            {
                return _station;
            }
            set
            {
                _station = value;
                OnPropertyChanged(() => Station);
            }
        }

        private double _additionalTime;

        public double AdditionalTime 
        {
            get { return _additionalTime; }
            set
            {
                _additionalTime = value;
                OnPropertyChanged(() => AdditionalTime);
            }
        }

        private double _additionalPrice;
        
        public double AdditionalPrice {
            get { return _additionalPrice; }

            set
            {
                _additionalPrice = value;
                OnPropertyChanged(() => AdditionalPrice);
            }
        }


        public StationOnRouteDTO() { }

        public StationOnRouteDTO(Station s)
        {
            Station = s;
        }

        public StationOnRouteDTO(Station s, double time, double price)
        {
            Station = s;
            AdditionalTime = time;
            AdditionalPrice = price;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> selectorExpression)
        {
            if (selectorExpression == null)
                throw new ArgumentNullException("selectorExpression");
            MemberExpression body = selectorExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The body must be a member expression");
            OnPropertyChanged(body.Member.Name);
        }


    }
}
