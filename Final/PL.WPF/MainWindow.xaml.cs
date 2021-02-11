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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL(1);
        public MainWindow()
        {
            InitializeComponent();
        }
        public void RefreshList(ListView list)
        {
            list.Items.Refresh();
        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            addBusWindow win = new addBusWindow(bl);
            win.Show();
        }

        private void addLine_Click(object sender, RoutedEventArgs e)
        {
            addLineWindow win = new addLineWindow(bl, this);
            win.Show();

        }
    }
}
