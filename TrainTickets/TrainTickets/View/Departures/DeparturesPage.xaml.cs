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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainTickets.dto;
using TrainTickets.model.station;
using TrainTickets.Services;

namespace TrainTickets.View.Departures
{
    /// <summary>
    /// Interaction logic for DeparturesPage.xaml
    /// </summary>
    public partial class DeparturesPage : Page
    {
        private DepartureService departureService = new DepartureService();
        public ObservableCollection<DepartureDTO> Lista { get; set; }
        public BindableCollection<Station> Stations { get; set; }
        private StationService stationService = new StationService();
        private Frame mainPage;
        private UserService userService;

        public DeparturesPage()
        {
            InitializeComponent();
        }

        public DeparturesPage(Frame mainPage, UserService userService)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.userService = userService;
            textBlock.Text = "Pregled svih vožnji";

            Stations = new BindableCollection<Station>(stationService.AllStations());

            Lista = new ObservableCollection<DepartureDTO>();
            foreach (var d in departureService.allDeparturesToDTO())
                Lista.Add(d);
            LV.ItemsSource = Lista;

            DataContext = this;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Station start = (Station)comboBox1.SelectedItem;
            Station end = (Station)comboBox2.SelectedItem;

            Lista.Clear();

            foreach (var v in departureService.departuresThatContainsStations(start, end))
                Lista.Add(v);

            LV.Items.Refresh();
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
        }

        private void KupljeneKarte_Click(object sender, RoutedEventArgs e)
        {

            var rowItem = (sender as Button).DataContext as DepartureDTO;
            TicketsForDeparture model = new TicketsForDeparture(rowItem);
            model.ShowDialog();

            // novi popup sa tabelom kupljnih karata za dati polazak
        }
    }
}
