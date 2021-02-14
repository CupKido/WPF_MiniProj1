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
        ManagerWindow Main;
        int ID;
        Double Lattitude;
        Double Longitude;
        BO.Station ThisStation;
        //for add
        public AddStationWindow(ManagerWindow main)
        {
            InitializeComponent();
            //For Refresh
            Main = main;

            UpdateButton.IsEnabled = false;
            UpdateButton.Opacity = 0;
            AddButton.IsEnabled = true;
            AddButton.Opacity = 1;
        }
        //for update
        public AddStationWindow(int Code, ManagerWindow main)
        {
            InitializeComponent();
            //For refresh
            Main = main;

            AddButton.IsEnabled = false;
            AddButton.Opacity = 0;
            UpdateButton.IsEnabled = true;
            UpdateButton.Opacity = 1;
            try
            {
                ThisStation = bl.GetStation(Code);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            IDTBO.Text = ThisStation.Code.ToString();
            IDTBO.IsEnabled = false;
            LongitudeTBO.Text = ThisStation.Longitude.ToString();
            LongitudeTBO.IsEnabled = false;
            LattitudeTBO.Text = ThisStation.Latitude.ToString();
            LattitudeTBO.IsEnabled = false;

            NameBO.Text = ThisStation.Name;
        }
        private void LattitudeTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Double.TryParse(LattitudeTBO.Text.Replace(',', '.'), out Lattitude) && LongitudeTBO.Text != "")
            {
                MessageBox.Show("numbers only!");
            }
            else
            {

            }
        }

        private void LongitudeTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Double.TryParse(LongitudeTBO.Text.Replace(',', '.'), out Longitude) && LongitudeTBO.Text != "")
            {
                MessageBox.Show("numbers only!");
            }
            else
            {

            }
        }

        private void IDTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(IDTBO.Text, out ID) && LattitudeTBO.Text != "")
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
                ThisStation = new BO.Station();
                ThisStation.Code = ID;
                ThisStation.Name = NameBO.Text;
                ThisStation.Latitude = Lattitude;
                ThisStation.Longitude = Longitude;
                try
                {
                    bl.AddStation(ThisStation);

                    this.Close();
                    Main.RefreshList(Main.StationsList);
                }
                catch (BO.BadStationIdException ex)
                {

                    MessageBox.Show("Not added\n ERROR: " + ex.Message);
                }


            }
            else { MessageBox.Show("please fill the empty filds"); }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ThisStation.Name = NameBO.Text;
            try
            {
                bl.UpdateStation(ThisStation);
                this.Close();
                Main.RefreshList(Main.StationsList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
