using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            // hh.Show();    //mode preko prozora (ako bude negde bio potreban)


            //web mode
            string curDir = Directory.GetCurrentDirectory();
            curDir = curDir.Substring(0, curDir.IndexOf("\\bin"));
            string path = String.Format("{0}\\HelpSystem\\Resources\\{1}.htm", curDir, key);
            ProcessStartInfo psi = new ProcessStartInfo      
            {
                FileName = path,
                UseShellExecute = true
            };
            try
            {
                Process.Start(psi);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
