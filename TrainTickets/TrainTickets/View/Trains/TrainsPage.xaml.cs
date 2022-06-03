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

namespace TrainTickets.View.Trains
{
    /// <summary>
    /// Interaction logic for TrainsPage.xaml
    /// </summary>
    public partial class TrainsPage : Page
    {
        public Frame MainPage { get; }


        public TrainsPage()
        {
            InitializeComponent();
        }

        public TrainsPage(Frame mainPage)
        {
            InitializeComponent();
            MainPage = mainPage;
        }

    }
}
