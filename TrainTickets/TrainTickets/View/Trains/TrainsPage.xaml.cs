using System;
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
using TrainTickets.model.train;
using TrainTickets.Services;

namespace TrainTickets.View.Trains
{
    /// <summary>
    /// Interaction logic for TrainsPage.xaml
    /// </summary>
    public partial class TrainsPage : Page
    { 
        public Frame MainPage { get; }

        private TrainService TrainService;

        public ObservableCollection<TrainDTO> Trains { get; set; }

        private bool editFlag = false;

        Point startPoint = new Point();
        public TrainsPage()
        {
            InitializeComponent();
        }

        public TrainsPage(Frame mainPage)
        {
            InitializeComponent();
            MainPage = mainPage;
            this.TrainService = new TrainService();
            this.Trains = new ObservableCollection<TrainDTO>();

            foreach (var t in TrainService.allTrainsToDTO())
                this.Trains.Add(t);

            TrainList.ItemsSource = this.Trains;
            DataContext = this;
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

            this.Trains.Clear();

            using (var db = new DatabaseContext())
            {
                foreach (var train in db.Trains)
                {

                    if (train.Name.ToLower().Contains(searchBox.Text.ToLower()))
                        this.Trains.Add(new TrainDTO(train.Name, train.Capacity));

                }
            }
        }

        private void textSearchStations_MouseDown(object sender, MouseButtonEventArgs e)
        {
            searchBoxStations.Focus();
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
                    Train train = (Train)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", train);
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

        private void btn_editTrain_Click(object sender, RoutedEventArgs e)
        {
            this.editFlag = true;
            TrainDTO selectedTrain = (TrainDTO)TrainList.SelectedItem;

            MainPage.Content = new AddOrEditTrain(MainPage, this.editFlag, selectedTrain);

            using (var db = new DatabaseContext())
            {
                foreach (var t in db.Trains)
                {
                    if (t.IsDeleted) continue;
                }
            }
        }

        private void btn_deleteTrain_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete voz?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return;

            TrainDTO row = (TrainDTO)TrainList.SelectedItem;
            if (!TrainService.deleteTrain(row))
            {
                MessageBox.Show("Greška pri brisanju linije!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.Trains.Clear();
            foreach (var t in TrainService.allTrainsToDTO())
                this.Trains.Add(t);
        }

        private void searchBoxStations_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StationsClicked(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void addRoute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TrainClicked(object sender, MouseButtonEventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                if (TrainList.SelectedItem == null) return;

                string name = ((TrainDTO)TrainList.SelectedItem).Name;
                Train selectedTrain = null;

                foreach (var train in db.Trains)
                {
                    if (name == train.Name)
                    {
                        selectedTrain = train;
                        break;
                    }
                }

                if (selectedTrain == null) return;

                btn_deleteTrain.Visibility = Visibility.Visible;
                btn_editTrain.Visibility = Visibility.Visible;

                switch (selectedTrain.Capacity) 
                {
                    case 24:
                        trainSeatsLayout24.Visibility = Visibility.Visible;
                        trainSeatsLayout26.Visibility = Visibility.Hidden;
                        trainSeatsLayout27.Visibility = Visibility.Hidden;
                        trainSeatsLayout31.Visibility = Visibility.Hidden;
                        break;

                    case 26:
                        trainSeatsLayout24.Visibility = Visibility.Hidden;
                        trainSeatsLayout26.Visibility = Visibility.Visible;
                        trainSeatsLayout27.Visibility = Visibility.Hidden;
                        trainSeatsLayout31.Visibility = Visibility.Hidden;
                        break;

                    case 27:
                        trainSeatsLayout24.Visibility = Visibility.Hidden;
                        trainSeatsLayout26.Visibility = Visibility.Hidden;
                        trainSeatsLayout27.Visibility = Visibility.Visible;
                        trainSeatsLayout31.Visibility = Visibility.Hidden;
                        break;

                    case 31:
                        trainSeatsLayout24.Visibility = Visibility.Hidden;
                        trainSeatsLayout26.Visibility = Visibility.Hidden;
                        trainSeatsLayout27.Visibility = Visibility.Hidden;
                        trainSeatsLayout31.Visibility = Visibility.Visible;
                        break;

                }

            }
        }

        private void addNewTrainBtn_Click(object sender, RoutedEventArgs e)
        {
            this.editFlag = false;

            MainPage.Content = new AddOrEditTrain(MainPage, this.editFlag);

            using (var db = new DatabaseContext())
            {
                foreach (var t in db.Trains)
                {
                    if (t.IsDeleted) continue;
                }
            }
        }
    }
}
