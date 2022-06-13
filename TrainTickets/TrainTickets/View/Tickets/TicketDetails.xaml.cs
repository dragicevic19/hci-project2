using GMap.NET;
using GMap.NET.WindowsPresentation;
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
using System.Windows.Shapes;
using TrainTickets.Database;
using TrainTickets.dto;
using TrainTickets.model;

namespace TrainTickets.View.Tickets
{
    /// <summary>
    /// Interaction logic for TicketDetails.xaml
    /// </summary>
    public partial class TicketDetails : Window
    {
        private RoutesForViewWithPriceDTO selectedRoute { get; set; }
        public ObservableCollection<StationOnRouteDTO> selectedStations { get; set; }
        public PointLatLng? previous { get; set; }


        public TicketDetails()
        {
            InitializeComponent();
        }

        public TicketDetails(User logUser, RoutesForViewWithPriceDTO selectedRoute)
        {
            InitializeComponent();
            this.selectedRoute = selectedRoute;
            this.selectedStations = new ObservableCollection<StationOnRouteDTO>();
            using (var db = new DatabaseContext())
            {
                foreach (var route in db.TrainRoutes)
                {
                    if (route.Deleted) continue;
                    if (route.Id == selectedRoute.Tr.Id)
                    {
                        foreach (var s in route.Stations)
                            this.selectedStations.Add(new StationOnRouteDTO(s.Station, s.AdditionalTime, s.AdditionalPrice));

                        routeNameTxt.Text = route.Name;
                        departureTimesTxt.Text = route.depTimesToStr();
                        trainTxt.Text = route.Train.Name;
                    }
                }
            }
            this.DataContext = this;
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            setUpMapView();
        }

        public void setUpMapView()
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            mapView.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            mapView.Markers.Clear();

            mapView.MinZoom = 3;
            mapView.MaxZoom = 17;
            mapView.Zoom = 7;
            mapView.Position = new PointLatLng(44.01583333, 20.55944444);
            mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            mapView.CanDragMap = true;
            mapView.DragButton = MouseButton.Left;
            mapView.ShowCenter = false;

            previous = null;

            bool startStation = selectedRoute.start.Id == selectedStations[0].Station.Id;
            bool endStation = false;

            foreach (var s in selectedStations)
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
                    drawLineBetweenPoints(current, previous, startStation, endStation);
                    previous = new PointLatLng(current.Lat, current.Lng);
                }
                if (s.Station.Id == selectedRoute.start.Id) startStation = true;
                if (s.Station.Id == selectedRoute.end.Id) endStation = true;
            }
        }

        private void drawLineBetweenPoints(PointLatLng current, PointLatLng? previous, bool startStation, bool endStation)
        {
            double dis = CountDistanceBetweenPoints(current, (PointLatLng)previous);
            if (dis < 0.01)
                return;
            PointLatLng middle = CountMiddlePoint(current, (PointLatLng)previous);
            GMapMarker markerLine = new GMapMarker(middle);
            Brush color;
            if (startStation && !endStation)
                color = Brushes.Green;
            else
                color = Brushes.Red;
            markerLine.Shape = new Ellipse
            {
                Width = 3,
                Height = 3,
                Stroke = color,
                StrokeThickness = 1.5,
                Visibility = Visibility.Visible,
                Fill = color,
            };
            mapView.Markers.Add(markerLine);
            drawLineBetweenPoints(current, middle, startStation, endStation);
            drawLineBetweenPoints((PointLatLng)previous, middle, startStation, endStation);
        }

        private double CountDistanceBetweenPoints(PointLatLng p1, PointLatLng p2)
        {
            return Math.Sqrt(Math.Pow(p1.Lat - p2.Lat, 2) + Math.Pow(p1.Lng - p2.Lng, 2));
        }

        private PointLatLng CountMiddlePoint(PointLatLng p1, PointLatLng p2)
        {
            return new PointLatLng((p1.Lat + p2.Lat) / 2, (p1.Lng + p2.Lng) / 2);
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
