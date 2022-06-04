using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TrainTickets.dto;
using TrainTickets.model.stationOnRoute;
using TrainTickets.model.trainRoute;
using TrainTickets.Services;

namespace TrainTickets.View.TrainRoutes
{
    /// <summary>
    /// Interaction logic for TrainRoutesPage.xaml
    /// </summary>
    public partial class TrainRoutesPage : Page
    {
        private Frame mainPage;
        public PointLatLng? previous { get; set; }

        private TrainRouteService TrainRouteService;

        public ObservableCollection<TrainRouteDTO> Routes { get; set; }


        public TrainRoutesPage()
        {
            InitializeComponent();
        }

        public TrainRoutesPage(Frame mainPage)
        {
            InitializeComponent();

            this.TrainRouteService = new TrainRouteService();
            this.Routes = new ObservableCollection<TrainRouteDTO>();

            using (var db = new DatabaseContext())
            {
                foreach (var route in db.TrainRoutes)
                {
                    this.Routes.Add(new TrainRouteDTO(route.Name, route.Stations[0].Station.Name, route.Stations[^1].Station.Name, route.DepartureTimes));
                }
            }
            RoutesList.ItemsSource = this.Routes;
            this.mainPage = mainPage;
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            setUpMapView();

        }

        public void setUpMapView()
        {

            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            // choose your provider here
            mapView.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            mapView.Markers.Clear();

            // don't forget to add the marker to the map
            //mapView.Markers.Add(routeMarker);

            mapView.MinZoom = 3;
            mapView.MaxZoom = 17;
            // whole world zoom
            mapView.Zoom = 7;
            mapView.Position = new GMap.NET.PointLatLng(44.01583333, 20.55944444);
            // lets the map use the mousewheel to zoom
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // lets the user drag the map
            mapView.CanDragMap = true;
            // lets the user drag the map with the left mouse button
            mapView.DragButton = MouseButton.Left;
            mapView.ShowCenter = false;

            previous = null;

            /*(foreach (Station s in stations2)
            {
                GMap.NET.PointLatLng current = new GMap.NET.PointLatLng(s.Latitude, s.Longitude);
                GMap.NET.WindowsPresentation.GMapMarker marker = new GMap.NET.WindowsPresentation.GMapMarker(current);

                marker.Shape = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Stroke = Brushes.Firebrick,
                    StrokeThickness = 1.5,
                    ToolTip = "Stanica",
                    Visibility = Visibility.Visible,
                    Fill = Brushes.Firebrick,

                };
                mapView.Markers.Add(marker);


                if (previous == null)
                {
                    previous = new PointLatLng(current.Lat, current.Lng);
                    continue;
                }
                else
                {
                    drawLineBetweenPoints(current, previous, mapView);
                    previous = new PointLatLng(current.Lat, current.Lng);
                }

            }*/
        }

        private void RouteClicked(object sender, MouseButtonEventArgs e)
        {
            mapView.Markers.Clear();
            previous = null;

            using (var db = new DatabaseContext())
            {
                if (RoutesList.SelectedItem == null)
                {
                    MessageBox.Show("Linija nije selektovana");
                    return;
                }

                string name = ((TrainRouteDTO)RoutesList.SelectedItem).Name;
                TrainRoute selectedRoute = null;

                foreach (var route in db.TrainRoutes)
                {
                    if (name == route.Name)
                    {
                        selectedRoute = route;
                        break;
                    }
                }

                if (selectedRoute == null) return;

                foreach (var s in selectedRoute.Stations)
                {

                    PointLatLng current = new PointLatLng(s.Station.Location.X, s.Station.Location.Y);
                    GMapMarker marker = new GMapMarker(current);

                    marker.Shape = new Ellipse
                    {
                        Width = 10,
                        Height = 10,
                        Stroke = Brushes.Red,
                        StrokeThickness = 1.5,
                        ToolTip = "Stanica " + s.Station.Name,
                        Visibility = Visibility.Visible,
                        Fill = Brushes.Red,

                    };
                    mapView.Markers.Add(marker);

                    if (previous == null)
                    {
                        previous = new PointLatLng(current.Lat, current.Lng);
                        continue;
                    }
                    else
                    {
                        drawLineBetweenPoints(current, previous);
                        previous = new PointLatLng(current.Lat, current.Lng);
                    }
                }
            }
        }

        private void drawLineBetweenPoints(PointLatLng current, PointLatLng? previous)
        {
            double dis = CountDistanceBetweenPoints(current, (PointLatLng)previous);
            if (dis < 0.001)
                return;
            PointLatLng middle = CountMiddlePoint(current, (PointLatLng)previous);
            GMapMarker markerLine = new GMapMarker(middle);
            markerLine.Shape = new Ellipse
            {
                Width = 3,
                Height = 3,
                Stroke = Brushes.YellowGreen,
                StrokeThickness = 1.5,
                Visibility = Visibility.Visible,
                Fill = Brushes.YellowGreen,
            };
            mapView.Markers.Add(markerLine);
            drawLineBetweenPoints(current, middle);
            drawLineBetweenPoints((PointLatLng)previous, middle);
        }


        private double CountDistanceBetweenPoints(PointLatLng p1, PointLatLng p2)
        {
            return Math.Sqrt(Math.Pow(p1.Lat - p2.Lat, 2) + Math.Pow(p1.Lng - p2.Lng, 2));
        }

        private PointLatLng CountMiddlePoint(PointLatLng p1, PointLatLng p2)
        {
            return new PointLatLng((p1.Lat + p2.Lat) / 2, (p1.Lng + p2.Lng) / 2);
        }


        private void mapView_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void mapView_Drop(object sender, DragEventArgs e)
        {

        }

        private void NewRouteBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void textSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            searchBox.Focus();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchBox.Text) && searchBox.Text.Length > 0)
                textSearch.Visibility = Visibility.Collapsed;
            else
                textSearch.Visibility = Visibility.Visible;

            this.Routes.Clear();

            using (var db = new DatabaseContext())
            {
                foreach (var route in db.TrainRoutes)
                {
                    if (route.Name.ToLower().Contains(searchBox.Text.ToLower()))
                        this.Routes.Add(new TrainRouteDTO(route.Name, route.Stations[0].Station.Name, route.Stations[^1].Station.Name, route.DepartureTimes));
                    else if (route.ContainsStation(searchBox.Text.ToLower()))
                        this.Routes.Add(new TrainRouteDTO(route.Name, route.Stations[0].Station.Name, route.Stations[^1].Station.Name, route.DepartureTimes));

                }
            }
        }

    }
}
