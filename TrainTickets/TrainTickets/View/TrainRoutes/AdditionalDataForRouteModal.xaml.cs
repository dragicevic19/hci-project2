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
using System.Windows.Shapes;

namespace TrainTickets.View.TrainRoutes
{
    /// <summary>
    /// Interaction logic for AdditionalDataForRouteModal.xaml
    /// </summary>
    public partial class AdditionalDataForRouteModal : Window
    {

        public double additionalTime { get; set; }
        public double additionalPrice { get; set; }

        public bool canceled = true;


        public AdditionalDataForRouteModal()
        {
            InitializeComponent();
        }

        public AdditionalDataForRouteModal(TrainRoutesPage w)
        {
            this.Owner = Window.GetWindow(w);
            InitializeComponent();
        }

        private void textAddTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtAddTime.Focus();
        }

        private void txtAddTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddTime.Text) && txtAddTime.Text.Length > 0)
                textAddTime.Visibility = Visibility.Collapsed;
            else
                textAddTime.Visibility = Visibility.Visible;
        }

        private void textAddPrice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            addPriceBox.Focus();
        }

        private void addPriceBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(addPriceBox.Text) && addPriceBox.Text.Length > 0)
                textAddPrice.Visibility = Visibility.Collapsed;
            else
                textAddPrice.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //validacija
            try
            {
                additionalPrice = Double.Parse(addPriceBox.Text);
                additionalTime = Double.Parse(txtAddTime.Text);
                canceled = false;
                this.Close();

            } catch(Exception ex)
            {
                MessageBox.Show("Neispravno uneti podaci!", "Nova linija", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
