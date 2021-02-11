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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class addBusWindow : Window
    {
        int temp = new int();
        IBL bl = BLFactory.GetBL(1);
        MainWindow Main;

        //for add
        public addBusWindow(MainWindow main)
        {
            InitializeComponent();

            //transition to add
            UpdateButton.IsEnabled = false;
            UpdateButton.Opacity = 0;
            addButton.IsEnabled = true;
            addButton.Opacity = 1;
        }
        //for update
        public addBusWindow(int LN, MainWindow main)
        {
            InitializeComponent();

            //transition to update
            addButton.IsEnabled = false;
            addButton.Opacity = 0;
            UpdateButton.IsEnabled = true;
            UpdateButton.Opacity = 1;

            //for refresh after finish
            Main = main;

            //for showing current info
            BO.BUS bus;
            try
            {
                bus = bl.GetBUS(LN);
                licenseTBO.Text = bus.LicenseNum.ToString();
                licenseTBO.IsEnabled = false;
                totalTripTBO.Text = bus.TotalTrip.ToString();
                licensingDP.DisplayDate = bus.FromDate;
                lastTreatmentDP.DisplayDate = bus.lastime;
                kmFromTreatTBO.Text = bus.ckm.ToString();
            }
            catch
            {
                MessageBox.Show("Bus Not found");
            }

            
            this.Show();
        }

        private void licenseTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (! int.TryParse(licenseTBO.Text, out temp))
            {
                MessageBox.Show("numbers only!");
            }
        }
        private void licenseTBO_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void AddBus(object sender, EventArgs e)
        {
            int LicenseNum = 0;
            int TotalKM = 0;
            bool flag = true;
            if(licensingDP.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Can't add bus\nplease select past or present time");
                flag = false;
            }
            if (!int.TryParse(licenseTBO.Text, out LicenseNum))
            {
                MessageBox.Show("numbers only!");
                flag = false;
            }
            if (!int.TryParse(totalTripTBO.Text, out TotalKM))
            {
                MessageBox.Show("numbers only!");
                flag = false;
            }
            if(lastTreatmentDP.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Can't add bus\nplease select past or present time");
                flag = false;
            }
            if (lastTreatmentDP.SelectedDate < licensingDP.SelectedDate)
            {
                MessageBox.Show("last treatment cannot be before licensing");
                flag = false;
            }
            if(!flag)
            {
                return;
            }
            BO.BUS bus = new BO.BUS{ LicenseNum = LicenseNum, FromDate = (DateTime)licensingDP.SelectedDate, lastime = (DateTime)lastTreatmentDP.SelectedDate, ckm = TotalKM };
            try
            {
                bl.AddBus(bus);
                this.Close();

            }
            catch
            {
                MessageBox.Show("not added");//ye   
            }

        }

        private void UpdateBus(object sender, RoutedEventArgs e)
        {
            int LicenseNum = 0;
            int TotalKM = 0;
            bool flag = true;
            if (licensingDP.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Can't add bus\nplease select past or present time");
                flag = false;
            }
            if (!int.TryParse(licenseTBO.Text, out LicenseNum))
            {
                MessageBox.Show("numbers only!");
                flag = false;
            }
            if (!int.TryParse(totalTripTBO.Text, out TotalKM))
            {
                MessageBox.Show("numbers only!");
                flag = false;
            }
            if (lastTreatmentDP.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Can't add bus\nplease select past or present time");
                flag = false;
            }
            if (lastTreatmentDP.SelectedDate < licensingDP.SelectedDate)
            {
                MessageBox.Show("last treatment cannot be before licensing");
                flag = false;
            }
            if (!flag)
            {
                return;
            }
            BO.BUS bus = new BO.BUS { LicenseNum = LicenseNum, FromDate = (DateTime)licensingDP.SelectedDate, lastime = (DateTime)lastTreatmentDP.SelectedDate, ckm = TotalKM };
            try
            {
                
            }
            catch
            {

            }
        }
    }
}
