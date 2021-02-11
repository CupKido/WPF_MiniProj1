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
using BL;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for ShowBusInfo.xaml
    /// </summary>
    public partial class ShowBusInfo : Window
    {
        MainWindow Main;
        BO.BUS ThisBus;
        public ShowBusInfo(BO.BUS bus, MainWindow main)
        {
            InitializeComponent();
            Main = main;
            ThisBus = bus;
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            addBusWindow win = new addBusWindow(ThisBus.LicenseNum, Main);
            win.Show();
            this.Close();
        }
    }
}
