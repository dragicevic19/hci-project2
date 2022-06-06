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
using TrainTickets.Database;

namespace TrainTickets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            //FillDB();
            pageLeft.Content = new PageForSignUp(pageLeft, pageRight, this);
            pageRight.Content = new LoginPage(pageLeft, pageRight, this);
        }


        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void pageLeft_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        #region fillDb
        private void FillDB()
        {
            using (var db = new DatabaseContext())
            {
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Aleksinac', 43.533, 21.717)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Apatin', 45.671, 18.985)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Beograd',  44.833, 20.500)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Bor',  44.100, 22.100)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Čačak', 43.892, 20.348)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Jagodina', 43.983, 21.250)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Kikinda', 45.830, 20.462)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Kragujevac', 44.017, 20.917)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Kraljevo', 43.733, 20.717)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Kruševac', 43.583,  21.333)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Loznica', 44.534,  19.224)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Negotin', 44.217,  22.533)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Novi Sad', 45.259,  19.833)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Niš', 43.317,  21.900)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Novi Pazar', 43.137,  20.512)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Pančevo', 44.867,  20.650)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Požarevac', 44.617,  21.200)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Priština', 42.667,  21.167)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Ruma', 45.008,  19.821)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Smederevo', 44.650,  20.933)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Sombor', 45.774, 19.112)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Subotica', 46.100376, 19.667587)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Sremska Mitrovica', 44.977, 19.615)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Užice', 43.861,  19.843)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Valjevo', 44.272, 19.885)");
                db.Database.ExecuteSqlCommand("Insert into Stations Values('Zrenjanjnin', 45.383, 20.391)");
                

                db.Database.ExecuteSqlCommand("Insert into TrainRoutes (Name, Deleted) Values('BN10A', 'false')");
                db.Database.ExecuteSqlCommand("Insert into TrainRoutes (Name, Deleted) Values('NB10A', 'false')");
                db.Database.ExecuteSqlCommand("Insert into TrainRoutes (Name, Deleted) Values('NN10', 'false')");
                db.Database.ExecuteSqlCommand("Insert into TrainRoutes (Name, Deleted) Values('LN20C', 'false')");
                db.Database.ExecuteSqlCommand("Insert into TrainRoutes (Name, Deleted) Values('SB10', 'false')");

                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('08:00:00.00', 1)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('10:00:00.00', 1)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('14:00:00.00', 1)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('19:00:00.00', 1)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('23:00:00.00', 1)");

                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('08:00:00.00', 2)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('10:00:00.00', 2)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('14:00:00.00', 2)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('19:00:00.00', 2)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('23:00:00.00', 2)");

                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('08:00:00.00', 3)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('10:00:00.00', 3)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('14:00:00.00', 3)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('19:00:00.00', 3)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('23:00:00.00', 3)");

                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('08:00:00.00', 4)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('10:00:00.00', 4)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('14:00:00.00', 4)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('19:00:00.00', 4)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('23:00:00.00', 4)");

                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('08:00:00.00', 5)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('10:00:00.00', 5)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('14:00:00.00', 5)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('19:00:00.00', 5)");
                db.Database.ExecuteSqlCommand("Insert into DepartureTimes (Time, RouteId) Values('23:00:00.00', 5)");

                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(3, 1, 0, 0)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(13, 1, 54, 1000)");

                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(13, 2, 0, 0)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(3, 2, 54, 1000)");

                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(13, 3, 0, 0)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(3, 3, 54, 1000)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(10, 3, 78, 1200)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(14, 3, 50, 800)");

                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(11, 4, 0, 0)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(23, 4, 60, 800)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(19, 4, 30, 400)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(13, 4, 40, 800)");

                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(22, 5, 0, 0)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(13, 5, 85, 1000)");
                db.Database.ExecuteSqlCommand("Insert into StationOnRoutes (StationId, TrainRouteId, AdditionalTime, AdditionalPrice) Values(3, 5, 30, 700)");

                db.SaveChanges();
            }
        }
        #endregion
    }
}
