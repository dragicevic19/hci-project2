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
using System.Windows.Shapes;
using TrainTickets.model;
using TrainTickets.Services;
using TrainTickets.View.Tickets;
using TrainTickets.View.TimeTable;
using TrainTickets.View.TrainRoutes;
using TrainTickets.View.Trains;

namespace TrainTickets.View.HomePage
{
    /// <summary>
    /// Interaction logic for HomePageWindow.xaml
    /// </summary>
    public partial class HomePageWindow : Window
    {
        private User user;
        private UserService userService;

        private Window loginWindow;

        public HomePageWindow()
        {
            InitializeComponent();
        }

        public HomePageWindow( UserService userService, Window loginWin)
        {
            InitializeComponent();
            this.Title = "HCI Voz - Menadžer";
            Uri iconUri = new Uri("../../../Images/train1.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            WindowState = WindowState.Maximized;
            this.loginWindow = loginWin;
            this.userService = userService;
            this.user = userService.logUser;
           
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if(Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }

            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            loginWindow.Show();
        }

        private void MaxBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                }
            }
        }

        private void Minimize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (WindowState != WindowState.Minimized) WindowState = WindowState.Minimized;
        }

        private void VozoviPressedHandler(object sender, MouseButtonEventArgs e)
        {
            mainPage.Content = new TrainsPage(mainPage, userService);
        }

        private void VozneLinijePressedHandler(object sender, MouseButtonEventArgs e)
        {
            mainPage.Content = new TrainRoutesPage(mainPage, userService);
        }

        private void RedVoznjePressedHandler(object sender, MouseButtonEventArgs e)
        {

            img_bg.Opacity = 0.3;
            mainPage.Content = new TicketsPage(mainPage, userService);

        }

        private void KartePressedHandler(object sender, MouseButtonEventArgs e)
        {
            img_bg.Opacity = 0.3;
            mainPage.Content = new TicketsPageForAdmin(mainPage);
        }

        private void OdjavaPressedHandler(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            loginWindow.Show();
        }

        private void BG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }



    }
}
