using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
using TrainTickets.model.train;
using TrainTickets.Services;

namespace TrainTickets.View.Trains
{
    /// <summary>
    /// Interaction logic for AddOrEditTrain.xaml
    /// </summary>
    public partial class AddOrEditTrain : Page
    {
        public Frame MainPage { get; }
        TrainDTO TrainToEdit;
        Point startPoint = new Point();
        bool EditFlag;
        private TrainService TrainService;
        public ObservableCollection<TrainDTO> Trains { get; set; }

        public AddOrEditTrain(Frame trainsPage, bool editFlag, TrainDTO selectedTrain)
        {
            InitializeComponent();

            this.EditFlag = editFlag;
            this.TrainToEdit = selectedTrain;
            TrainName.Text = selectedTrain.Name;
            TrainCapacity.Text = selectedTrain.Capacity.ToString();

            this.TrainService = new TrainService();
            this.Trains = new ObservableCollection<TrainDTO>();
        }

        public AddOrEditTrain(Frame trainsPage, bool editFlag)
        {
            InitializeComponent();

            this.EditFlag = editFlag;
            TrainName.Text = "";
            TrainCapacity.Text = "";
            TrainName.IsEnabled = true;
            layout24.Visibility = Visibility.Hidden;
            layout26.Visibility = Visibility.Hidden;
            layout27.Visibility = Visibility.Hidden;
            layout31.Visibility = Visibility.Hidden;

            this.TrainService = new TrainService();
            this.Trains = new ObservableCollection<TrainDTO>();
            this.TrainToEdit = new TrainDTO();
        }

        private void layout24Preview_MouseEnter(object sender, MouseEventArgs e)
        {
            layout24Preview.Width = 213;
            layout24Preview.Height = 231;
            layout24Border.BorderBrush = Brushes.Red;
        }

        private void layout26Preview_MouseEnter(object sender, MouseEventArgs e)
        {
            layout26Preview.Width = 213;
            layout26Preview.Height = 231;
            layout26Border.BorderBrush = Brushes.Red;
        }

        private void layout27Preview_MouseEnter(object sender, MouseEventArgs e)
        {
            layout27Preview.Width = 213;
            layout27Preview.Height = 231;
            layout27Border.BorderBrush = Brushes.Red;
        }

        private void layout31Preview_MouseEnter(object sender, MouseEventArgs e)
        {
            layout31Preview.Width = 213;
            layout31Preview.Height = 231;
            layout31Border.BorderBrush = Brushes.Red;
        }

        private void layout24Preview_MouseLeave(object sender, MouseEventArgs e)
        {
            layout24Preview.Width = 212;
            layout24Preview.Height = 230;
            layout24Border.BorderBrush = Brushes.Aqua;
        }

        private void layout26Preview_MouseLeave(object sender, MouseEventArgs e)
        {
            layout26Preview.Width = 212;
            layout26Preview.Height = 230;
            layout26Border.BorderBrush = Brushes.Aqua;
        }

        private void layout27Preview_MouseLeave(object sender, MouseEventArgs e)
        {
            layout27Preview.Width = 212;
            layout27Preview.Height = 230;
            layout27Border.BorderBrush = Brushes.Aqua;
        }

        private void layout31Preview_MouseLeave(object sender, MouseEventArgs e)
        {
            layout31Preview.Width = 212;
            layout31Preview.Height = 230;
            layout31Border.BorderBrush = Brushes.Aqua;
        }

        private void layout24Preview_MouseDown(object sender, MouseButtonEventArgs e)
        {
            layout24.Visibility = Visibility.Visible;
            layout26.Visibility = Visibility.Hidden;
            layout27.Visibility = Visibility.Hidden;
            layout31.Visibility = Visibility.Hidden;
            this.TrainToEdit.Capacity = 24;
            TrainCapacity.Text = "24";
        }

        private void layout26Preview_MouseDown(object sender, MouseButtonEventArgs e)
        {
            layout26.Visibility = Visibility.Visible;
            layout24.Visibility = Visibility.Hidden;
            layout27.Visibility = Visibility.Hidden;
            layout31.Visibility = Visibility.Hidden;
            this.TrainToEdit.Capacity = 26;
            TrainCapacity.Text = "26";
        }

        private void layout27Preview_MouseDown(object sender, MouseButtonEventArgs e)
        {
            layout27.Visibility = Visibility.Visible;
            layout26.Visibility = Visibility.Hidden;
            layout24.Visibility = Visibility.Hidden;
            layout31.Visibility = Visibility.Hidden;
            this.TrainToEdit.Capacity = 27;
            TrainCapacity.Text = "27";
        }

        private void layout31Preview_MouseDown(object sender, MouseButtonEventArgs e)
        {
            layout31.Visibility = Visibility.Visible;
            layout26.Visibility = Visibility.Hidden;
            layout27.Visibility = Visibility.Hidden;
            layout24.Visibility = Visibility.Hidden;
            this.TrainToEdit.Capacity = 31;
            TrainCapacity.Text = "31";
        }

        private void zavrsiBtnClick(object sender, RoutedEventArgs e)
        {

            if (TrainName.Text == null)
            {
                MessageBox.Show("Ime voza je obavezno!", "Dodavanje voza", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (TrainCapacity.Text == null)
            {
                MessageBox.Show("Kapacitet voza je obavezan!", "Dodavanje voza", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (this.EditFlag)
                {
                    if (this.TrainService.editTrain(TrainName.Text, TrainCapacity.Text))
                    {
                        MessageBox.Show("Uspešno ste izmenili voz!", "Izmena voza", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Greška pri izmeni voza!", "Izmena voza", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    if (!this.TrainService.addTrain(TrainName.Text, TrainCapacity.Text))
                    {
                        MessageBox.Show("Voz sa unetim imenom već postoji!", "Dodavanje voza", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Uspešno ste dodali novi voz!", "Dodavanje voza", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                foreach (var t in TrainService.allTrainsToDTO())
                    this.Trains.Add(t);

            }
        }
    }
}
