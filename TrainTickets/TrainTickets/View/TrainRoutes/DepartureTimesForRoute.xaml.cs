﻿using System;
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
using TrainTickets.model.trainRoute;

namespace TrainTickets.View.TrainRoutes
{
    /// <summary>
    /// Interaction logic for DepartureTimesForRoute.xaml
    /// </summary>
    public partial class DepartureTimesForRoute : Page
    {
        public bool canceled = true;

        public static List<DepartureTime> departureTimes = new List<DepartureTime>();
        public static string name;


        public DepartureTimesForRoute()
        {
            InitializeComponent();
        }

        public DepartureTimesForRoute(TrainRoutesPage w)
        {
            InitializeComponent();
        }

        private void textDepTimes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            depTimesBox.Focus();
        }

        private void depTimesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(depTimesBox.Text) && depTimesBox.Text.Length > 0)
                textDepTimes.Visibility = Visibility.Collapsed;
            else
                textDepTimes.Visibility = Visibility.Visible;
        }

        private void textName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            nameBox.Focus();
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nameBox.Text) && nameBox.Text.Length > 0)
                textName.Visibility = Visibility.Collapsed;
            else
                textName.Visibility = Visibility.Visible;
        }

        public void ParseDepTimes()
        {
            try
            {
                departureTimes.Clear();
                string input = depTimesBox.Text;
                input = input.Trim();
                foreach(var i in input.Split(';'))
                {
                    if (i == "") continue;
                    string[] hoursAndMins = i.Split(':');
                    int hours = int.Parse(hoursAndMins[0]);
                    int minutes = int.Parse(hoursAndMins[1]);

                    if (hours > 23 || minutes > 59) throw new Exception();

                    TimeSpan ts = new TimeSpan(hours, minutes, 0);
                    departureTimes.Add(new DepartureTime(ts));
                }

                name = nameBox.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neispravno uneti podaci!");
            }
        }
    }
}
