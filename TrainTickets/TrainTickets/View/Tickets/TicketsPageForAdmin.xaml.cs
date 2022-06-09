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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrainTickets.View.Tickets
{
    /// <summary>
    /// Interaction logic for TicketsPageForAdmin.xaml
    /// </summary>
    public partial class TicketsPageForAdmin : Page
    {
        private Frame mainPage;

        public TicketsPageForAdmin()
        {
            InitializeComponent();
        }

        public TicketsPageForAdmin(Frame mainPage)
        {
            this.mainPage = mainPage;
        }
    }
}
