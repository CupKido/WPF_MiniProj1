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
    public partial class AddBusWin : Window
    {
       
        public AddBusWin()
        {
            InitializeComponent();
            DP.SelectedDate = DateTime.Now;
            DP.DisplayDateEnd = DateTime.Now;
        }

        private void addbus1_Click(object sender, RoutedEventArgs e)
        {
            DateTime SDate;
            string license;
            int Ilicense = 0;
            double IKM = 0;
            bool isokay = true;
            bool chekbox = false;
            BUSES Buses = DataContext as BUSES;
            

            SDate = DP.SelectedDate.Value.Date;
            if (SDate > DateTime.Now)
            {
                MessageBox.Show("ERROR: you cant add an bus in the future");
                isokay = false;
            }

            license = license_num.Text;
            bool flag = int.TryParse(license, out Ilicense);
            if(!flag)
            {
                MessageBox.Show("ERROR: Use Numbers ONLY");
                isokay = false;
            }
            if(SDate.Year < 2018)
            {
                if(license.Length != 7)
                {
                    MessageBox.Show("ERROR: 7 numbers needed");
                    isokay = false;
                }
            }
            if (SDate.Year >= 2018)
            {
                if (license.Length != 8)
                {
                    MessageBox.Show("ERROR: 8 numbers needed");
                    isokay = false;
                }
            }
            if(Buses.indexByID(license) != -1)
            {
                MessageBox.Show("ERROR: Bus allready in list");
                isokay = false;
            }
            if (check_km.IsChecked.Value)
            {
                flag = double.TryParse(kilometraj.Text, out IKM);
                if (!flag)
                {
                    MessageBox.Show("ERROR: Use Numbers ONLY");
                    isokay = false;
                }
            }
            if (isokay)
            {
                 BUS nbus = new BUS(license, 0, SDate, SDate);
                if (check_km.IsChecked.Value)
                {
                    nbus = new BUS(license, IKM, SDate, SDate);
                }
                else
                {
                    nbus = new BUS(license, 0, SDate, SDate);
                }
                Buses.Add(nbus);
                nbus.updateMW(addbus1.DataContext as MainWindow);
                this.Close();
            }

        }
        private void AddBusEnter(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                addbus1_Click(sender, null);
            }
        }
        private void check_km_Checked(object sender, RoutedEventArgs e)
        {
            kilometraj.IsEnabled = true;
        }

        private void check_km_Unchecked(object sender, RoutedEventArgs e)
        {
            kilometraj.IsEnabled = false;
            kilometraj.Text = "";
        }
    }
}
