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
        public addBusWindow()
        {
            InitializeComponent();
            
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
    }
}
