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
using TrainTickets.dto;
using TrainTickets.model;
using TrainTickets.model.departure;
using TrainTickets.Services;

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
        private RoutesForViewWithPriceDTO rfv;
        private DepartureService departureService = new DepartureService();
        private TicketService ticService = new TicketService();
  


        public BuyTicket(User u,RoutesForViewWithPriceDTO rfv)
        {
            InitializeComponent();
            DateTime d = DateTime.Today;
            for(int i = 0; i<7; i++)
            {
                comboBox1.Items.Add(d.AddDays(i).ToString("dd/MM/yyyy"));
            }
            this.rfv = rfv;
            this.u = u;
            textBlock.Text = "Kupi kartu za: " + rfv.start + " - " + rfv.end + " u " + rfv.startTime + ".\n"+"Izaberi datum:";
        }

       
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati vreme polaska", "Kupovina karte", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            String t = comboBox1.SelectedItem.ToString() + " " + rfv.startTime.ToString(@"hh\:mm");
            DateTime dt = DateTime.ParseExact(t, "g", new System.Globalization.CultureInfo("fr-FR"));
            if (dt > DateTime.Now)
            {
                Departure dwp = new Departure();
                dwp = (Departure)departureService.findDeparture(rfv.Tr, dt);
                if (dwp == null)
                {
                    departureService.createDeparture(rfv.Tr, dt);
                    dwp = departureService.findDeparture(rfv.Tr, dt);
                }

                if (ticService.createTicket(rfv, u, dwp.Id, true))
                {
                    MessageBox.Show("Uspešno kupljena karta.", "Kupovina karte", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nema više slobodnih mesta.", "Kupovina karte", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ovaj ruta je vec prošla!", "Kupovina karte", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}
