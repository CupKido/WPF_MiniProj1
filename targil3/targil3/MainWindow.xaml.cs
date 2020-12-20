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
        public void refresh()
        {
            buslist.ItemsSource = Buses.ToList();
        }
        private void addbus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWin window = new AddBusWin();
            window.DataContext = Buses;
            window.addbus1.DataContext = this;
            window.Show();
            
        }

        private void GoForRide(object sender, RoutedEventArgs e)
        {
            NewRideWindow window = new NewRideWindow();
            BUS Bus = (sender as Button).DataContext as BUS;
            if(Bus.inprocc)
            {
                MessageBox.Show("ERROR: Bus allready in ride!");
            }else
            if(Bus.treatmentneeded(0))
            {
                MessageBox.Show("ERROR: Please Repair Bus");
            }
            else
            {
                Bus.updateMW(this);
                window.GazAmount.Text = Bus.pGaz;
                window.go.DataContext = Bus;
                window.Show();
            }
            

        }
        private void refillGazThreads(object sender, RoutedEventArgs e)
        {
            BUS Bus = (sender as Button).DataContext as BUS;
            Bus.updateMW(this);
            if (Bus.inprocc)
            {
                MessageBox.Show("ERROR: Bus in ride!");
            }
            else
            {
                (Buses.index(Buses.index(Bus)).updateMW(this)).refillGazThreads();
            }
            
        }
        private void RepairThreads(object sender, RoutedEventArgs e)
        {
            BUS Bus = (sender as Button).DataContext as BUS;
            if (Bus.inprocc)
            {
                MessageBox.Show("ERROR: Bus in ride!");
            }
            else
            {
                (Buses.index(Buses.index(Bus)).updateMW(this)).RepairThreads();
            }
        }

        private void GoForRide_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ShowBus(object sender, MouseEventArgs e)
        {
            BUS Bus = buslist.SelectedItem as BUS;
            string textbox = "Bus ID: " + Bus.pID + "\nBus Starting date: " + Bus.pStartDate + "\nLast Repair Date: " + Bus.plastDate;
            textbox = textbox + "\nGaz tank: " + Bus.pGaz + "\nOverAll killometers: " + Bus.pkm + "\nKillometers From Last Repair: " + Bus.pckm;
            textbox = textbox + "\nRepair Needed?: ";
            if(Bus.treatmentneeded(0))
            {
                textbox = textbox + "yes";
            }else { textbox = textbox + "no"; }

            BusDetails Det = new BusDetails();
            Det.Details.Text = textbox;
            
            Det.Repair.DataContext = (Buses.index(Buses.index(Bus)).updateMW(this));
            Det.Remove.DataContext = Buses;
            Det.DataContext = this;
            Det.Show();
        }
        public void RemoveTBus(object sender, RoutedEventArgs e)
        {
            RemoveBus window = new RemoveBus();
            window.DataContext = Buses;
            window.Remove.DataContext = this;
            window.Show();
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
