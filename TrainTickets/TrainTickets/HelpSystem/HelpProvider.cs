using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using TrainTickets;
using TrainTickets.View.TrainRoutes;

namespace HelpSystem
{
    public class HelpProvider
    {
        public static string GetHelpKey(DependencyObject obj)
        {
            return obj.GetValue(HelpKeyProperty) as string;
        }

        public static void SetHelpKey(DependencyObject obj, string value)
        {
            obj.SetValue(HelpKeyProperty, value);
        }

        public static readonly DependencyProperty HelpKeyProperty =
            DependencyProperty.RegisterAttached("HelpKey", typeof(string), typeof(HelpProvider), new PropertyMetadata("index", HelpKey));
        private static void HelpKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //NOOP
        }

        public static void ShowHelp(string key, TrainRoutesPage originator)
        {
            HelpViewer hh = new HelpViewer(key, originator);
            //hh.Show();    mode preko prozora (ako bude negde bio potreban)
            var psi = new ProcessStartInfo      //web mode
            {
                FileName = "file:///C:/Users/sovil/Downloads/index.htm",        //ovo je sad samo za test, na odbrani cemo imati drugu putanju na lokalu
                UseShellExecute = true
            };
            Process.Start(psi);   
        }
    }
}
