using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TrainTickets.dto;
using TrainTickets.model.ticket;
using TrainTickets.Services;

namespace TrainTickets.View.TimeTable
{
    /// <summary>
    /// Interaction logic for UserTicket.xaml
    /// </summary>
    public partial class UserTicket : Page
    {
        private Frame mainPage;
        private TicketService ticketService = new TicketService();
        public ObservableCollection<TicketViewDTO> Lista { get; set; }
        private UserService userService;
        public UserTicket()
        {
            InitializeComponent();
        }

        public UserTicket(Frame mainPage,UserService userService)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.userService = userService;
            textBlock.Text = "Sve kupljene karte korisnika: " + userService.logUser.Email.ToString();
            Lista = new ObservableCollection<TicketViewDTO>();
            foreach (var l in ticketService.listToDTOList(ticketService.allPurchasedUserTickets(userService.logUser)))
                Lista.Add(l);
            LV.ItemsSource = Lista;
            DataContext = this;
        }
    }
}
