using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using TrainTickets.model.trainSeats;

namespace TrainTickets.dto
{
    public class TrainSeatsDTO
    {
        private List<OneSeat> _seats;
        public List<OneSeat> Seats
        {
            get
            {
                return _seats;
            }
            set
            {
                _seats = value;
                OnPropertyChanged(() => Seats);
            }
        }

        public TrainSeatsDTO() { }

        public TrainSeatsDTO(List<OneSeat> s)
        {
            Seats = s;
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

