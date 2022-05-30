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
    /// Interaction logic for PageForSignIn.xaml
    /// </summary>
    public partial class PageForSignIn : Page
    {
        Frame page_left, page_right;


        public PageForSignIn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.page_left.Content = new PageForSignUp(page_left, page_right);
            this.page_right.Content = new LoginPage(page_left, page_right);
        }

        public PageForSignIn(Frame l, Frame r) {
            InitializeComponent();
            this.page_left = l;
            this.page_right = r;
        }
    }
}
