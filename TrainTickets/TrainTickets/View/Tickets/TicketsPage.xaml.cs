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
using TrainTickets.Services;

namespace TrainTickets.View.Tickets
{
    /// <summary>
    /// Interaction logic for TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        private Frame mainPage;
        private List<Station> stations;
        private StationService stationService = new StationService();


        public TicketsPage()
        {
            InitializeComponent();
            this.stations = stationService.AllStations();

        }

        public TicketsPage(Frame mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
        }
    }
}
