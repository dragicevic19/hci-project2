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
using TrainTickets.Services;
using TrainTickets.View.HomePage;

namespace TrainTickets
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private Frame page_left;
        private Frame page_right;
        private Window mainWindow;

        private UserService userService;

        public LoginPage()
        {
            InitializeComponent();
        }

        public LoginPage(Frame pageLeft, Frame pageRight, Window mainWindow)
        {
            InitializeComponent();
            this.page_left = pageLeft;
            this.page_right = pageRight;
            this.mainWindow = mainWindow;
            this.userService = new UserService();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(passwordBox.Password))
            {
                bool isLogin = userService.Login(txtEmail.Text, passwordBox.Password);

                if (isLogin == false)
                {
                    MessageBox.Show("Netačna lozinka ili email!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
              
                    if (userService.findByEmail(txtEmail.Text).UserType == model.UserType.Manager)
                    {
                        mainWindow.Hide();
                        Window homeWindow = new HomePageWindow( userService, mainWindow);
                        homeWindow.Show();
                    }
                    else
                    {

                        mainWindow.Hide();
                        Window homeWindow = new HomePageWindowUser( userService, mainWindow);
                        homeWindow.Show();
                    }
                }
            }
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
