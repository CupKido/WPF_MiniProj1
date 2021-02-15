using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class addLineWindow : Window
    {
        int lastStation;
        int firstStation;
        int ID;
        IBL bl = BLFactory.GetBL(1);
        ManagerWindow Main;
        BO.Line ThisLine;

        //for Add
        public addLineWindow(ManagerWindow main)
        {
            InitializeComponent();
            Main = main;


            //for add
            //To Show
            addButton.Visibility = Visibility.Visible;

            //To Hide
            UpdateButton.Visibility = Visibility.Collapsed;

            //combo box preps
            List<string> CBSource = new List<string>();
            CBSource.Add("north");
            CBSource.Add("center");
            CBSource.Add("south");
            areaCB.ItemsSource = CBSource;
        }

        //For Update
        public addLineWindow(int ID, int Code, ManagerWindow main)
        {
            InitializeComponent();
            Main = main;

            //combo box preps
            List<string> CBSource = new List<string>();
            CBSource.Add("north");
            CBSource.Add("center");
            CBSource.Add("south");
            areaCB.ItemsSource = CBSource;

            //for update

            //To Show
            UpdateButton.Visibility = Visibility.Visible;

            //To Hide
            addButton.Visibility = Visibility.Collapsed;

            try
            {
                ThisLine = bl.GetLine(ID, Code);
            }
            catch(BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ID);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
            IDTBO.Text = ThisLine.ID.ToString();
            IDTBO.IsEnabled = false;
            firstStationTBO.Text = ThisLine.FirstStation.ToString();
            lastStationTBO.Text = ThisLine.LastStation.ToString();
            areaCB.SelectedItem = ThisLine.Area.ToString();

        }
        private void lastStationTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(lastStationTBO.Text, out lastStation))
            {
                MessageBox.Show("numbers only!");
            }
        }

        private void firstStationTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(firstStationTBO.Text, out firstStation))
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
            if (!((IDTBO.Text == null) || (firstStationTBO.Text == null) || (lastStationTBO.Text == null)))
            {
                BO.Line line = new BO.Line();
                line.ID = ID;
                line.FirstStation = firstStation;
                line.LastStation = lastStation;
                switch (areaCB.SelectedItem)
                {
                    case "north":
                        line.Area = BO.Areas.north;
                        break;
                    case "center":
                        line.Area = BO.Areas.center;
                        break;
                    case "south":
                        line.Area = BO.Areas.south;
                        break;
                    default:
                        line.Area = BO.Areas.center;
                        break;
                }
                ;
                try
                {
                    bl.AddLine(line);

                    Main.RefreshList(Main.LinesList);
                    this.Close();
                }
                catch (BO.BadLineIdException ex)
                {

                    MessageBox.Show("Not added\n ERROR: " + ex.Message);
                }


            }
            else { MessageBox.Show("please fill the empty filds"); }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            if (int.TryParse(firstStationTBO.Text, out temp))
            {
                ThisLine.FirstStation = temp;
            }
            if (int.TryParse(lastStationTBO.Text, out temp))
            {
                ThisLine.LastStation = temp;
            }
            switch (areaCB.SelectedItem)
            {
                case "north":
                    ThisLine.Area = BO.Areas.north;
                    break;
                case "center":
                    ThisLine.Area = BO.Areas.center;
                    break;
                case "south":
                    ThisLine.Area = BO.Areas.south;
                    break;
                default:
                    ThisLine.Area = BO.Areas.center;
                    break;
            }
                ;
            bl.UpdateLine(ThisLine);
            Main.RefreshList(Main.LinesList);
            this.Close();
        }
    }
}
