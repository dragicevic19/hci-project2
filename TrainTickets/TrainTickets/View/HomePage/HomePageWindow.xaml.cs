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
using TrainTickets.View.Trains.AddTrain;
using TrainTickets.View.Trains.ShowTrains;

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

        public HomePageWindow(string email, UserService userService, Window loginWin)
        {
            InitializeComponent();
            this.loginWindow = loginWin;
            this.userService = userService;
            this.user = userService.findByEmail(email);

            var menuTrains = new List<SubItem>();
            menuTrains.Add(new SubItem("Dodaj novi voz", new AddTrainPage()));
            menuTrains.Add(new SubItem("Prikazi vozove", new ShowTrainsPage()));
            var item0 = new ItemMenu("Vozovi", menuTrains, "/Images/train1.png");
            /*
            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("Services"));
            menuSchedule.Add(new SubItem("Meetings"));
            var item1 = new ItemMenu("Appointments", menuSchedule, PackIconKind.Schedule);

            var menuReports = new List<SubItem>();
            menuReports.Add(new SubItem("Customers"));
            menuReports.Add(new SubItem("Providers"));
            menuReports.Add(new SubItem("Products"));
            menuReports.Add(new SubItem("Stock"));
            menuReports.Add(new SubItem("Sales"));
            var item2 = new ItemMenu("Reports", menuReports, PackIconKind.FileReport);

            var menuExpenses = new List<SubItem>();
            menuExpenses.Add(new SubItem("Fixed"));
            menuExpenses.Add(new SubItem("Variable"));
            var item3 = new ItemMenu("Expenses", menuExpenses, PackIconKind.ShoppingBasket);

            var menuFinancial = new List<SubItem>();
            menuFinancial.Add(new SubItem("Cash flow"));
            var item4 = new ItemMenu("Financial", menuFinancial, PackIconKind.ScaleBalance);

            */

            st_pnl.Children.Add(new UserControlMenuItem(item0, this));
            //st_pnl.Children.Add(new UserControlMenuItem(item1, this));
            //st_pnl.Children.Add(new UserControlMenuItem(item2, this));
            //st_pnl.Children.Add(new UserControlMenuItem(item3, this));
            //st_pnl.Children.Add(new UserControlMenuItem(item4, this));
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }


        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if(Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }

            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 0.3;
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
    }
}
