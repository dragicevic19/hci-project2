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

namespace TrainTickets.View.TrainRoutes
{
    /// <summary>
    /// Interaction logic for TrainRoutesPage.xaml
    /// </summary>
    public partial class TrainRoutesPage : Page
    {
        private Frame mainPage;

        public TrainRoutesPage()
        {
            InitializeComponent();
        }

        public TrainRoutesPage(Frame mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
        }
    }
}
