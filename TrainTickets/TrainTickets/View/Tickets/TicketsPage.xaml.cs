using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainTickets.model.station;
using TrainTickets.model; 
using TrainTickets.Services;
using Caliburn.Micro;
using TrainTickets.dto;

namespace TrainTickets.View.Tickets
{
    /// <summary>
    /// Interaction logic for TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        private Frame mainPage;
        private List<Station> stations;                 //visak kasnije kad budemo zakucali stanice

        private StationService stationService = new StationService();
        private RouteService routeService = new RouteService();

        public BindableCollection<Station> Stations { get; set; }

        BindableCollection<RoutesForViewWithPriceDTO> RoutesForView { get; set; }


        public TicketsPage()
        {



            InitializeComponent();


        }

        public TicketsPage(Frame mainPage)
        {
            //ovo sve je za brisanje
            this.stations = new List<Station>();
            this.stations.Add(new Station(1, "BEOGRAD", new Location(1000, 2000)));
            this.stations.Add(new Station(2, "NIS", new Location(1200, 2200)));
            this.stations.Add(new Station(3, "LOZNICA", new Location(1300, 2400)));
            this.stations.Add(new Station(4, "NOVI SAD", new Location(1400, 2500)));
            Stations = new BindableCollection<Station>(stations);

            DataContext = this;

            //Stations = new BindableCollection<Station>(stationService.AllStations());
            InitializeComponent();
            this.mainPage = mainPage;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Station start = (Station)comboBox1.SelectedItem;
            Station end = (Station)comboBox2.SelectedItem;
            if (start==null  || end == null)
                MessageBox.Show("popuni oba polja");
            else if (start.Equals(end))
                MessageBox.Show("pocetno i krajnje ne mogu biti isti");
            else
            {

                this.RoutesForView = new BindableCollection<RoutesForViewWithPriceDTO>(routeService.routesWithPriceAndTime(start, end));
                DataContext = this;

                //metoda koja ce vracati sve rute koje odgovaraju za prikaz
            }

        }
    }
}
