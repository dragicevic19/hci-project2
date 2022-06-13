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
using TrainTickets.Services;

namespace TrainTickets.View.UserTickets
{
    /// <summary>
    /// Interaction logic for UserReservations.xaml
    /// </summary>
    public partial class UserReservations : Page
    {
        private Frame mainPage;
        private TicketService ticketService = new TicketService();
        public ObservableCollection<TicketViewDTO> Lista { get; set; }
        private UserService userService;
        public UserReservations()
        {
            InitializeComponent();
        }

        public UserReservations(Frame mainPage, UserService userService)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.userService = userService;
            textBlock.Text = "Sve kupljene karte korisnika: " + userService.logUser.Email.ToString();
            this.nap();
        }

        private void nap()
        {
            Lista = new ObservableCollection<TicketViewDTO>();
            foreach (var l in ticketService.listToDTOList(ticketService.allReservationUserTickets(userService.logUser)))
                Lista.Add(l);
            LV.ItemsSource = Lista;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as TicketViewDTO;
            if (ticketService.purished(rowItem.ticket.Id))
            {
                this.nap();
                MessageBox.Show("Uspešno kupljena karta", "Karta kupljena", MessageBoxButton.OK, MessageBoxImage.Information); }

            else
                MessageBox.Show("Greska pri kupovini.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
