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
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace TrainTickets.View.Tickets
{
    /// <summary>
    /// Interaction logic for TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        private Frame mainPage;
        private List<Station> stations;
        public BindableCollection<Station> Stations { get; set; }

        private StationService stationService = new StationService();
        private RouteService routeService = new RouteService();
        private UserService userService;

         
        public ObservableCollection<RoutesForViewWithPriceDTO> Lista { get; set; }

        public TicketsPage()
        {
            InitializeComponent();
        }

        public TicketsPage(Frame mainPage, UserService userService)
        {
            InitializeComponent();

            Stations = new BindableCollection<Station>(stationService.AllStations());
            this.userService = userService;

            Lista = new ObservableCollection<RoutesForViewWithPriceDTO>();
            foreach (var l in routeService.allRoutes())
                Lista.Add(l);
 
            LV.ItemsSource = this.Lista;
            DataContext = this;

            if (userService.logUser.UserType == UserType.Manager)
            {
                kupiColumn.Width = 0;
                rezColumn.Width = 0;
            }
            
            this.mainPage = mainPage;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Station start = (Station)comboBox1.SelectedItem;
            Station end = (Station)comboBox2.SelectedItem;
            if (start == null || end == null)
            {
                MessageBox.Show("Popuni oba polja!", "Red vožnje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (start.Equals(end))
                MessageBox.Show("Početna i krajnja stanica ne mogu biti iste", "Red vožnje", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Lista.Clear();
                foreach (var v in routeService.routesWithPriceAndTime(start, end))
                    Lista.Add(v);
                DataContext = this;

                LV.Items.Refresh();
                //metoda koja ce vracati sve rute koje odgovaraju za prikaz
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as RoutesForViewWithPriceDTO;
            BuyTicket model = new BuyTicket(this.userService.logUser, rowItem);
            model.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as RoutesForViewWithPriceDTO;
            ReservationTicket model = new ReservationTicket(this.userService.logUser, rowItem);
            model.ShowDialog();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as RoutesForViewWithPriceDTO;
            TicketDetails model = new TicketDetails(this.userService.logUser, rowItem);
            model.ShowDialog();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
                btn.Visibility = Visibility.Visible;
            }
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                btn.Visibility = Visibility.Visible;
            }
        }
    }
}
