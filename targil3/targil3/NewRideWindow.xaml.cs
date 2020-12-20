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
    public partial class NewRideWindow : Window
    {
        BUS Bus = new BUS();
        public NewRideWindow()
        {
            InitializeComponent();

        }
        private void AddRide(object sender, RoutedEventArgs e)
        {
            string Snkm = kmToGo.Text;
            int nkm;
            bool ok = int.TryParse(Snkm, out nkm);
            if(ok == false)
            {
                MessageBox.Show("ERROR: represent KM as numbers");
            }
            else
            {
                
               // Bus = go.DataContext as BUS;
                Bus = (sender as Button).DataContext as BUS;
                if(Bus.treatmentneeded(nkm))
                {
                    MessageBox.Show("ERROR: cannot make ride, please repair the bus");
                    this.Close();
                }
                if (1200 < nkm)
                {
                    MessageBox.Show("ERROR: cannot make ride, the Gaz tank too small");
                } else
                if (Bus.currentGaz < nkm)
                {
                    MessageBox.Show("ERROR: cannot make ride, please refill the Gaz tank");
                    this.Close();
                }
                Bus.addride(nkm);

                this.Close();
            }
            
        }
    }
}
