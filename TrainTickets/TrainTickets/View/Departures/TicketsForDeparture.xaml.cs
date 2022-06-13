using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TrainTickets.model.station;
using TrainTickets.Services;

namespace TrainTickets.View.Departures
{
    /// <summary>
    /// Interaction logic for TicketsForDeparture.xaml
    /// </summary>
    public partial class TicketsForDeparture : Window
    {
        private TicketService ticketService = new TicketService();
        public ObservableCollection<TicketViewDTO> Lista { get; set; }
        public BindableCollection<Station> Stations { get; set; }
        private StationService stationService = new StationService();


        public TicketsForDeparture() {
            InitializeComponent();
        }

        public TicketsForDeparture(DepartureDTO selectedDeparture)
        {
            InitializeComponent();
            Lista = new ObservableCollection<TicketViewDTO>();
            foreach (var l in ticketService.listToDTOList(ticketService.allTicketsForDeparture(selectedDeparture.DepartureId)))
                Lista.Add(l);
            LV.ItemsSource = Lista;
            titleText.Text = "Prodate karte za izabrani polazak: " + selectedDeparture.StartStation + " - " + selectedDeparture.EndStation + "\n" + selectedDeparture.date + " u " + selectedDeparture.startTime;
            DataContext = this;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
