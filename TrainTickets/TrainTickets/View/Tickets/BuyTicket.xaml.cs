using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrainTickets.model;
using TrainTickets.model.departure;

namespace TrainTickets.View.Tickets
{
    /// <summary>
    /// Interaction logic for BuyTicket.xaml
    /// </summary>
    public partial class BuyTicket : Window
    {
       
        private Departure dep;
        private User u;
        private List<DateTime> times;
  


        public BuyTicket(User u)
        {
            InitializeComponent();
            DateTime d = DateTime.Today;
            for(int i = 0; i<7; i++)
            {
                comboBox1.Items.Add(d.AddDays(i).ToString("dd/MM/yyyy"));
               
            }
 
      

            this.u = u;
            
            
        }

        public BuyTicket(Departure d, User u)
        {
            this.dep = d;
            this.u = u;
            
            InitializeComponent();
        }
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //dodati rezervaciju
        }
    }
}
