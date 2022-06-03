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

namespace TrainTickets.View.Tickets
{
    /// <summary>
    /// Interaction logic for TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        private Frame mainPage;

        public TicketsPage()
        {
            InitializeComponent();
        }

        public TicketsPage(Frame mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
        }
    }
}
