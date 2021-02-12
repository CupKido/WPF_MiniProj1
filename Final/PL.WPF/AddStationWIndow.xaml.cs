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
    public partial class AddStationWindow : Window
    {
        IBL bl = BLFactory.GetBL(1);
        MainWindow Main;
        int ID;
        int Lattitude;
        int Longitude;
        public AddStationWindow()
        {
            InitializeComponent();
        }
        public AddStationWindow(int Code, MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }
        private void LattitudeTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(LattitudeTBO.Text, out Lattitude))
            {
                MessageBox.Show("numbers only!");
            }
            else
            {

            }
        }

        private void LongitudeTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(LongitudeTBO.Text, out Longitude))
            {
                MessageBox.Show("numbers only!");
            }
            else
            {

            }
        }

        private void IDTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(IDTBO.Text, out ID))
            {
                MessageBox.Show("numbers only!");
            }
            else
            {

            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!((IDTBO.Text == null) || (LongitudeTBO.Text == null) || (LattitudeTBO.Text == null)))
            {
                BO.Station station = new BO.Station();
                station.Code = ID;
                station.Longitude = Longitude;
                station.Latitude = Lattitude;
                station.Name = NameBO.Text.ToString();
                try
                {
                    //bl.AddStation(station);

                    this.Close();
                }
                catch (BO.BadLineIdException ex)
                {

                    MessageBox.Show("Not added\n ERROR: " + ex.Message);
                }


            }
            else { MessageBox.Show("please fill the empty filds"); }
        }
    }
}
