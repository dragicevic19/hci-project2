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

        private StationService stationService = new StationService();
        public MonthlyReview()
        {
            InitializeComponent();
        }

        public MonthlyReview(Frame mainPage, UserService userService)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.userService = userService;
            textBlock.Text = "Sve kupljene karte za izabrani mesec: ";
            
            Stations = new BindableCollection<Station>(stationService.AllStations());
    
             
            Lista = new ObservableCollection<TicketViewDTO>();
            foreach (var l in ticketService.listToDTOList(ticketService.allTicketsMon(null,null,true)))
                Lista.Add(l);
            LV.ItemsSource = Lista;
            
            DataContext = this;



        }

        private void OnClick(object sender, RoutedEventArgs e)
        {

            Station start = (Station)comboBox1.SelectedItem;
            Station end = (Station)comboBox2.SelectedItem;
            

                Lista.Clear();
                foreach (var v in ticketService.listToDTOList(ticketService.allTicketsMon(start, end, true)))
                    Lista.Add(v);
                DataContext = this;

                LV.Items.Refresh();
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem  = null;



                //metoda koja ce vracati sve rute koje odgovaraju za prikaz
        }

        
 
        
    }
}
