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
using System.Windows.Shapes;

namespace targil3B
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BusDetails : Window
    {
        public BusDetails()
        {
            InitializeComponent();
        }
        private void RepairThreads(object sender, RoutedEventArgs e)
        {
            BUS Bus = Repair.DataContext as BUS;
            Bus.updateBD(this);
            Bus.RepairThreads();
            
        }
        private void refillGazThreads(object sender, RoutedEventArgs e)
        {
            BUS Bus = Repair.DataContext as BUS;
            Bus.updateBD(this);
            Bus.refillGazThreads();

        }
        public void RemoveTBus(object sender, RoutedEventArgs e)
        {
            BUSES Buses = Remove.DataContext as BUSES;
            BUS Bus = Repair.DataContext as BUS;
            Buses.RemoveBus(Bus.currentID);
            Bus.updateMW(DataContext as MainWindow);
            this.Close();


        }
        public void ShowBus(BUS Bus)
        {
            
            string textbox = "Bus ID: " + Bus.pID + "\nBus Starting date: " + Bus.pStartDate + "\nLast Repair Date: " + Bus.plastDate;
            textbox = textbox + "\nGaz tank: " + Bus.pGaz + "\nOverAll killometers: " + Bus.pkm + "\nKillometers From Last Repair: " + Bus.pckm;
            textbox = textbox + "\nRepair Needed?: ";
            if (Bus.treatmentneeded(0))
            {
                textbox = textbox + "yes";
            }
            else { textbox = textbox + "no"; }

            Details.Text = textbox;


        }
    }
}
