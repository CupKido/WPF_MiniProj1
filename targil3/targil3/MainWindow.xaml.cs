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


namespace targil3B
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime today = DateTime.Now;
        BUSES Buses = new BUSES();
        BUS currentBus = new BUS();
        
        
        public MainWindow()
        {
            InitializeComponent();
            Buses.Add10Randoms(today);
            
            buslist.ItemsSource = Buses.ToList();
            
            //ShowBusLine(0);
        }

        private void addbus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWin window = new AddBusWin();
            window.Show();
        }

        private void GoForRide(object sender, RoutedEventArgs e)
        {
            NewRideWindow window = new NewRideWindow();
            window.Show();
        }
        private void refillGazThreads(object sender, RoutedEventArgs e)
        {
            BUS Bus = (sender as Button).DataContext as BUS;
            Bus.updateMW(this);
            (Buses.index(Buses.index(Bus)).updateMW(this)).refillGazThreads();
            
        }
        private void RepairThreads(object sender, RoutedEventArgs e)
        {
            BUS Bus = (sender as Button).DataContext as BUS;
            (Buses.index(Buses.index(Bus)).updateMW(this)).RepairThreads();

        }

        private void GoForRide_Click(object sender, RoutedEventArgs e)
        {

        }
        //private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        // {
        //     ShowBusLine((cbBusLines.SelectedValue as BusLine));
        //
        // }
        //  private void ShowBusLine(int bus)
        //  {
        //      currentBus = Buses.index(bus);
        //      UpGrid.DataContext = currentBus;
        //      lbBusLineStations.DataContext = Buses.index(bus).GStations;
        //      tbArea.Text = currentShowed.AreaToString();
        //  }
        //  private void ShowBusLine(BusLine bus)
        //  {
        //
        //      currentShowed = bus;
        //      UpGrid.DataContext = currentShowed;
        //      InitializeComponent();
        //      lbBusLineStations.DataContext = bus.GStations;
        //      tbArea.Text = currentShowed.AreaToString();
        //  }
        //  private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //  {
        //      buslist.ItemsSource = Buses;
        //  }
    }
}
