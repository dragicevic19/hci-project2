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

namespace TrainTickets
{
    /// <summary>
    /// Interaction logic for PageForSignUp.xaml
    /// </summary>
    public partial class PageForSignUp : Page
    {
        private Frame page_left, page_right;
        private Window mainWindow;

        public PageForSignUp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.page_left.Content = new PageForSignIn(this.page_left, this.page_right, mainWindow);
            this.page_right.Content = new RegistrationPage(page_left, page_right, mainWindow);
        }

        public PageForSignUp(Frame pageLeft, Frame pageRight, Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.page_left = pageLeft;
            this.page_right = pageRight;
        }
    }
}
