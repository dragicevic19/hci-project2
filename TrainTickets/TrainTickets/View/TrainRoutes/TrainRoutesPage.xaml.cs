using Caliburn.Micro;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using HelpSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using TrainTickets.model;
using TrainTickets.model.station;
using TrainTickets.model.stationOnRoute;
using TrainTickets.model.train;
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
        private UserService userService;

        private User currentUser;

        public BindableCollection<Train> Trains { get; set; }

        public ObservableCollection<TrainRouteDTO> Routes { get; set; }

        Point startPoint = new Point();
        private ObservableCollection<Station> allStations;
        public ObservableCollection<StationOnRouteDTO> selectedStations { get; set; }

        public DepartureTimesForRoute nameAndDepTimesControl;

        private TrainRoute newTrainRoute;
        private bool editFlag = false;

        private TrainService trainService;

        public TrainRoutesPage()
        {
            InitializeComponent();
        }

        public TrainRoutesPage(Frame mainPage, UserService userService)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.userService = userService;
            this.currentUser = userService.logUser;
            this.trainService = new TrainService();
            this.TrainRouteService = new TrainRouteService();
            this.Routes = new ObservableCollection<TrainRouteDTO>();
            this.selectedStations = new ObservableCollection<StationOnRouteDTO>();

            Trains = new BindableCollection<Train>(trainService.getAll());

            comboTrain.SelectedItem = Trains[0];

            foreach (var r in TrainRouteService.allRoutesToDTO())
                this.Routes.Add(r);

            RoutesList.ItemsSource = this.Routes;
            DataContext = this;

            addNewRouteBtn.Visibility = (currentUser.UserType == UserType.Client) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            setUpMapView();
        }

        public void setUpMapView()
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            // choose your provider here
            mapView.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            mapView.Markers.Clear();

            // don't forget to add the marker to the map
            //mapView.Markers.Add(routeMarker);

            mapView.MinZoom = 3;
            mapView.MaxZoom = 17;
            // whole world zoom
            mapView.Zoom = 8;
            mapView.Position = new PointLatLng(44.01583333, 20.55944444);
            // lets the map use the mousewheel to zoom
            mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            // lets the user drag the map
            mapView.CanDragMap = true;
            // lets the user drag the map with the left mouse button
            mapView.DragButton = MouseButton.Left;
            mapView.ShowCenter = false;

            previous = null;

            foreach (var s in selectedStations) // kad se obrise stanica pozivam ovu metodu da mi nacrta novu putanju
            {

                PointLatLng current = new PointLatLng(s.Station.Location.X, s.Station.Location.Y);
                GMapMarker marker = new GMapMarker(current)
                {
                    Shape = new Ellipse
                    {
                        Width = 10,
                        Height = 10,
                        Stroke = Brushes.Red,
                        StrokeThickness = 1.5,
                        ToolTip = "Stanica " + s.Station.Name,
                        Visibility = Visibility.Visible,
                        Fill = Brushes.Red,

                    }
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

        private void RouteClicked(object sender, MouseButtonEventArgs e)
        {
            mapView.Markers.Clear();
            previous = null;

            using (var db = new DatabaseContext())
            {
                if (RoutesList.SelectedItem == null) return;

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

                btn_editLine.Visibility = (currentUser.UserType == UserType.Client) ? Visibility.Collapsed : Visibility.Visible;
                btn_deleteLine.Visibility = (currentUser.UserType == UserType.Client) ? Visibility.Collapsed : Visibility.Visible;

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
            if (dis < 0.01)
                return;
            PointLatLng middle = CountMiddlePoint(current, (PointLatLng)previous);
            GMapMarker markerLine = new GMapMarker(middle);
            markerLine.Shape = new Ellipse
            {
                Width = 3,
                Height = 3,
                Stroke = Brushes.Green,
                StrokeThickness = 1.5,
                Visibility = Visibility.Visible,
                Fill = Brushes.Green,
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


        private void NewRouteBtnClick(object sender, RoutedEventArgs e)
        {
            this.addNewRouteBtn.Visibility = Visibility.Collapsed;
            this.showRoutesPanel.Visibility = Visibility.Hidden;
            mapView.Markers.Clear();
            previous = null;

            newTrainRoute = new TrainRoute();

            this.addRoutePanel.Visibility = Visibility.Visible;
            this.nameAndDepTimesControl = new DepartureTimesForRoute(this);
            this.NameAndDepartureFrame.Content = nameAndDepTimesControl;

            this.allStations = new ObservableCollection<Station>();
            using(var db = new DatabaseContext())
            {
                this.allStations = new ObservableCollection<Station>(db.Stations.ToList());
            }

            this.StationsList.ItemsSource = this.allStations;
        }

        private void btn_deleteLine_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete liniju?", "Brisanje linije", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return;

            TrainRouteDTO row = (TrainRouteDTO) RoutesList.SelectedItem;
            if (TrainRouteService.CanEditOrDelete(row))
            {
                if (!TrainRouteService.deleteRoute(row))
                {
                    MessageBox.Show("Greška pri brisanju linije!", "Brisanje linije", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                this.Routes.Clear();
                foreach (var r in TrainRouteService.allRoutesToDTO())
                    this.Routes.Add(r);

                mapView.Markers.Clear();
                previous = null;
            }
            else
            {
                MessageBox.Show("Trenutno je nemoguće izbrisati liniju jer je kupljena/rezervisana od strane klijenta!", "Brisanje linije", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void btn_editLine_Click(object sender, RoutedEventArgs e)
        {
            this.editFlag = true;
            TrainRouteDTO selectedRoute = (TrainRouteDTO)RoutesList.SelectedItem;

            this.addNewRouteBtn.Visibility = Visibility.Collapsed;
            this.showRoutesPanel.Visibility = Visibility.Hidden;
            mapView.Markers.Clear();
            previous = null;


            this.addRoutePanel.Visibility = Visibility.Visible;
            this.nameAndDepTimesControl = new DepartureTimesForRoute(this);
            this.nameAndDepTimesControl.nameBox.Text = selectedRoute.Name;
            this.nameAndDepTimesControl.nameBox.IsReadOnly = true;
            this.nameAndDepTimesControl.nameBox.ToolTip = "Ne možete izmeniti naziv linije";

            this.nameAndDepTimesControl.depTimesBox.Text = selectedRoute.DepartureTimes;
            this.NameAndDepartureFrame.Content = nameAndDepTimesControl;

            foreach(var train in Trains)
            {
                if (train.Name == trainService.findByName(selectedRoute.TrainName).Name)
                {
                    comboTrain.SelectedItem = train;
                    break;
                }
            }
            
            comboTrain.IsEnabled = false;
            comboTrain.ToolTip = "Ne možete izmeniti voz!";


            this.allStations = new ObservableCollection<Station>();
            using (var db = new DatabaseContext())
            {
                this.allStations = new ObservableCollection<Station>(db.Stations.ToList());
            }

            this.StationsList.ItemsSource = this.allStations;
            TrainRoute t = TrainRouteService.FindByName(selectedRoute.Name);
            this.selectedStations.Clear();

            using(var db = new DatabaseContext())
            {
                foreach(var r in db.TrainRoutes)
                {
                    if (r.Deleted) continue;
                    if (r.Name == selectedRoute.Name)
                    {
                        foreach(var s in r.Stations)
                        {
                            this.selectedStations.Add(new StationOnRouteDTO(s.Station, s.AdditionalTime, s.AdditionalPrice));
                        }
                    }
                }
            }

            if (!TrainRouteService.CanEditOrDelete(selectedRoute))
            {
                MessageBox.Show("Karta za ovu liniju je rezervisana ili kupljena. Nije moguca izmena podataka već samo pregled!", "Izmena linije" ,MessageBoxButton.OK, MessageBoxImage.Information);
            }
            setUpMapView();
        }
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);
            }
        }

        #region dodavanje nove linije

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
                    if (!route.Deleted)
                    {
                        if (route.Name.ToLower().Contains(searchBox.Text.ToLower()))
                            this.Routes.Add(new TrainRouteDTO(route.Name, route.Stations[0].Station.Name, route.Stations[^1].Station.Name, route.DepartureTimes, route.Train.Name));
                        else if (route.ContainsStation(searchBox.Text.ToLower()))
                            this.Routes.Add(new TrainRouteDTO(route.Name, route.Stations[0].Station.Name, route.Stations[^1].Station.Name, route.DepartureTimes, route.Train.Name));
                    }
                }
            }
        }

        private void textSearchStations_MouseDown(object sender, MouseButtonEventArgs e)
        {
            searchBoxStations.Focus();
        }

        private void searchBoxStations_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchBoxStations.Text) && searchBoxStations.Text.Length > 0)
                textSearchStations.Visibility = Visibility.Collapsed;
            else
                textSearchStations.Visibility = Visibility.Visible;


            allStations.Clear();

            using (var db = new DatabaseContext())
            {
                foreach (var station in db.Stations)
                {
                    if (station.Name.ToLower().Contains(searchBoxStations.Text.ToLower()))
                    {
                        allStations.Add(station);
                    }
                }
            }
        }

        private void StationsClicked(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                     Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    // Get the dragged ListViewItem
                    ListView listView = sender as ListView;
                    ListViewItem listViewItem =
                        FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                    // Find the data behind the ListViewItem
                    Station station = (Station)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", station);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
            catch (Exception)
            {

            }
        }
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void mapView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private bool IsStationInSelectedStations(Station station)
        {
            foreach(var s in selectedStations)
            {
                if (s.Station.Id == station.Id) return true;
            }
            return false;
        }

        private void mapView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Station station = e.Data.GetData("myFormat") as Station;
                if (IsStationInSelectedStations(station))
                {
                    MessageBox.Show("Ovu stanicu ste vec dodali!", "Dodavanje stanice", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                StationOnRouteDTO newStationOnRoute = new StationOnRouteDTO(station);

                if (selectedStations.Count > 0) // ako nije pocetna stanica
                {
                    AdditionalDataForRouteModal modal = new AdditionalDataForRouteModal(this);
                    modal.ShowDialog();

                    if (modal.canceled) return;

                    newStationOnRoute.AdditionalTime = modal.additionalTime;
                    newStationOnRoute.AdditionalPrice = modal.additionalPrice;
                }

                this.selectedStations.Add(newStationOnRoute);
                addNewMarker(station);
                this.allStations.Remove(station);
            }
        }


        private double calcMinutesFromTimeSpan(TimeSpan additionalTime)
        {
            int hours = additionalTime.Hours;
            int minutes = additionalTime.Minutes;
            return (hours == 0) ? minutes : hours * 60 + minutes;
        }

        private void addNewMarker(Station s)
        {
            PointLatLng current = new PointLatLng(s.Location.X, s.Location.Y);
            GMapMarker marker = new GMapMarker(current)
            {
                Shape = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Stroke = Brushes.Red,
                    StrokeThickness = 1.5,
                    ToolTip = "Stanica " + s.Name,
                    Visibility = Visibility.Visible,
                    Fill = Brushes.Red,
                }
            };
            mapView.Markers.Add(marker);


            if (previous == null)
            {
                previous = new PointLatLng(current.Lat, current.Lng);
            }
            else
            {
                drawLineBetweenPoints(current, previous);
                previous = new PointLatLng(current.Lat, current.Lng);
            }
        }

        private void addRoute_Click(object sender, RoutedEventArgs e)
        {
            nameAndDepTimesControl.ParseDepTimes();

            if (!DepartureTimesForRoute.valid)
            {
                return;
            }
            else if (selectedStations.Count < 2)
            {
                MessageBox.Show("Morate odabrati bar 2 stanice za liniju", "Nova linija", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (editFlag)
                {
                    if (TrainRouteService.CanEditOrDelete(new TrainRouteDTO(DepartureTimesForRoute.name)))
                    {
                        if (TrainRouteService.editRoute(selectedStations, DepartureTimesForRoute.name, DepartureTimesForRoute.departureTimes))
                        {
                            MessageBox.Show("Uspešno ste izmenili liniju!", "Linija izmenjena", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Greška pri izmeni linije!", "Izmena linije", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Karta za ovu linija je kupljena/rezervisana i nemoguće je izmeniti je!", "Izmena linije", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    Train train = (Train)comboTrain.SelectedItem;
                    if (!TrainRouteService.addRoute(selectedStations, DepartureTimesForRoute.name, DepartureTimesForRoute.departureTimes, train))
                    {
                        MessageBox.Show("Linija sa unetim imenom već postoji!", "Nova linija", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Uspešno ste dodali novu liniju!", "Linija dodata!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                editFlag = false;
                this.nameAndDepTimesControl.nameBox.IsReadOnly = false;
                this.nameAndDepTimesControl.nameBox.ToolTip = "Unesite naziv nove linije";
                this.addNewRouteBtn.Visibility = Visibility.Visible;
                this.showRoutesPanel.Visibility = Visibility.Visible;
                mapView.Markers.Clear();
                previous = null;

                comboTrain.IsEnabled = true;

                this.addRoutePanel.Visibility = Visibility.Hidden;
                this.Routes.Clear();
                foreach (var r in TrainRouteService.allRoutesToDTO())
                    this.Routes.Add(r);
                this.selectedStations.Clear();
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete stanicu?", "Brisanje stanice", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            StationOnRouteDTO row = (StationOnRouteDTO) DataGrid.SelectedItem;

            if (selectedStations[0].Station.Id == row.Station.Id)
            {
                if (selectedStations.Count > 1)
                {
                    selectedStations[1].AdditionalTime = 0;
                    selectedStations[1].AdditionalPrice = 0;
                }
            }
            this.selectedStations.Remove(row);
            setUpMapView();
        }
        #endregion

    }
    
}