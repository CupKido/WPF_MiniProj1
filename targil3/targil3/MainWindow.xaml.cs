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


namespace targil3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private  BLines Buses = new BLines();
        private BusLine allstats = new BusLine("XXXXXX", null, null);
        private BusLine currentShowed = new BusLine();
        public MainWindow()
        {

            Buses.RandomStart40ten(allstats);
            InitializeComponent();
            cbBusLines.ItemsSource = Buses;
            cbBusLines.SelectedIndex = 0;
            ShowBusLine(0);
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine));

        }
        private void ShowBusLine(int bus)
        {
            currentShowed = Buses.index(bus);
            UpGrid.DataContext = currentShowed;
            lbBusLineStations.DataContext = Buses.index(bus).GStations;
            tbArea.Text = currentShowed.AreaToString();
        }
        private void ShowBusLine(BusLine bus)
        {

            currentShowed = bus;
            UpGrid.DataContext = currentShowed;
            InitializeComponent();
            lbBusLineStations.DataContext = bus.GStations;
            tbArea.Text = currentShowed.AreaToString();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
