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
    /// Interaction logic for RemoveBus.xaml
    /// </summary>
    public partial class RemoveBus : Window
    {
        public RemoveBus()
        {
            InitializeComponent();
        }
        private void EnterRemove(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                RemoveTBus(sender, null);
            }
        }
        private void RemoveTBus(object sender, RoutedEventArgs e)
        {
            int temp;
            string ID  = license_num.Text;
            if (int.TryParse(ID, out temp))
            {
                if(ID.Length > 8 || ID.Length < 7)
                {
                    MessageBox.Show("ERROR: no such license number");
                }
                else
                {
                    BUSES Buses = DataContext as BUSES;
                    if (!Buses.RemoveBus(ID))
                    {
                        MessageBox.Show("ERROR: Bus not found");
                    }
                    else
                    {
                        new BUS().updateMW(Remove.DataContext as MainWindow);
                        this.Close();
                    }

                }

            }
        }
    }
}
