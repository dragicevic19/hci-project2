using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
using TrainTickets.dto;
using TrainTickets.Services;

namespace TrainTickets
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private Frame left_frame, right_frame;
        private Window mainWindow;
        private UserService userService;

        public RegistrationPage()
        {
            InitializeComponent();
        }

        public RegistrationPage(Frame l, Frame r, Window mainWindow)
        {
            InitializeComponent();
            this.left_frame = l;
            this.right_frame = r;
            this.mainWindow = mainWindow;
            this.userService = new UserService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (validation(txtEmail.Text, passwordBox.Password, confPasswordBox.Password, txtFirstName.Text, txtLastName.Text))
            {
                RegisterUserDTO user = new RegisterUserDTO(txtEmail.Text, passwordBox.Password, txtFirstName.Text, txtLastName.Text);
                bool isRegistered = userService.Register(user);

                if (isRegistered)
                {
                    MessageBox.Show("Registracija uspešna. Sada se možete prijaviti!", "Registracija", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.left_frame.Content = new PageForSignUp(left_frame, right_frame, mainWindow);
                    this.right_frame.Content = new LoginPage(left_frame, right_frame, mainWindow);
                }
                else
                {
                    MessageBox.Show("Uneta email adresa već postoji u sistemu!", "Registracija", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        } 
        

        private bool validation(string email, string password, string confPassword, string name, string lastName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confPassword) ||
                string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Sva polja su obavezna!", "Registracija", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (FormatException)
            {
                MessageBox.Show("Neispravan format email adrese!", "Registracija", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (confPassword != password)   // ubaciti neke poruke u .xaml fajl pa ih ovde prikazati samo
            {
                MessageBox.Show("Lozinke se ne poklapaju!", "Registracija", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("U polje <ime> je moguće uneti samo slova!", "Registracija", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("U polje <prezime> je moguće uneti samo slova!", "Registracija", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void textFirstName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFirstName.Focus();
        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFirstName.Text) && txtFirstName.Text.Length > 0)
                textFirstName.Visibility = Visibility.Collapsed;
            else
                textFirstName.Visibility = Visibility.Visible;
        }

        private void textLastName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLastName.Focus();
        }

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text.Length > 0)
                textLastName.Visibility = Visibility.Collapsed;
            else
                textLastName.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textConfPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            confPasswordBox.Focus();
        }

        private void confPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(confPasswordBox.Password) && confPasswordBox.Password.Length > 0)
                textConfPassword.Visibility = Visibility.Collapsed;
            else
                textConfPassword.Visibility = Visibility.Visible;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
