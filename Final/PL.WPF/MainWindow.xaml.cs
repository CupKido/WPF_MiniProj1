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

using BL;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL(1);
        ObservableCollection<BO.BUS> ObserListOfBuses = new ObservableCollection<BO.BUS>();
        public MainWindow()
        {
            InitializeComponent();

            foreach(var item in bl.GetAllBuses())
            {
                ObserListOfBuses.Add(item);
            }

        }

        public void Click_OpenBuses(object sender, RoutedEventArgs e)
        {
            Buses buses = new Buses();
            buses.Show();
        }
        public void Click_OpenLines(object sender, RoutedEventArgs e)
        {
            Lines lines = new Lines();
            lines.Show();
        }
        public void Click_OpenStations(object sender, RoutedEventArgs e)
        {
            Stations stations = new Stations();
            stations.Show();
        }
    }

    

}
