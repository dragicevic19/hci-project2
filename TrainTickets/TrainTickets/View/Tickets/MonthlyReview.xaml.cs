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

namespace TrainTickets.View.Tickets
{
    /// <summary>
    /// Interaction logic for MonthlyReview.xaml
    /// </summary>
    public partial class MonthlyReview : Page
    {
        private Frame mainPage;
        private TicketService ticketService = new TicketService();
        public ObservableCollection<TicketViewDTO> Lista { get; set; }
        private UserService userService;

        public BindableCollection<Station> Stations { get; set; }
        public BindableCollection<DateTime> t { get; set; }


        private StationService stationService = new StationService();
       

        public MonthlyReview(Frame mainPage, UserService userService)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.userService = userService;
            textBlock.Text = "Sve kupljene karte za izabrani mesec: ";
            DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);
            LastMonthFirstDate = LastMonthFirstDate.AddMonths(1);
            for (int i = 0; i < 12; i++)
            {
                comboBox3.Items.Add(LastMonthFirstDate);
                

                LastMonthFirstDate = LastMonthFirstDate.AddMonths(-1);

            }
            
            Stations = new BindableCollection<Station>(stationService.AllStations());
    
             
            Lista = new ObservableCollection<TicketViewDTO>();
            foreach (var l in ticketService.listToDTOList(ticketService.allTicketsMon(null,null,(DateTime)comboBox3.Items[0])))
                Lista.Add(l);
            LV.ItemsSource = Lista;
            
            DataContext = this;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Station start = (Station)comboBox1.SelectedItem;
            Station end = (Station)comboBox2.SelectedItem;
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati mesec!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Lista.Clear();
            foreach (var v in ticketService.listToDTOList(ticketService.allTicketsMon(start, end,(DateTime) comboBox3.SelectedItem)))
                Lista.Add(v);
            DataContext = this;

            LV.Items.Refresh();
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem  = null;
        }
    }
}
