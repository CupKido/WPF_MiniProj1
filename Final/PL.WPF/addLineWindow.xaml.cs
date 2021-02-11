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
        IBL bl;
        MainWindow Main;
        public addLineWindow(IBL newbl,MainWindow main)
        {
            InitializeComponent();
            bl = newbl;
            Main = main;
            List<string> CBSource = new List<string>();
            CBSource.Add("north");
            CBSource.Add("center");
            CBSource.Add("south");
            areaCB.ItemsSource = CBSource;
        }

        private void lastStationTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(lastStationTBO.Text, out lastStation))
            {
                MessageBox.Show("numbers only!");
            }
            else
            {

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
                    
                    Main.subjectsList.Items.Refresh();
                    this.Close();
                }
                catch(BO.BadLineIdException ex)
                {

                    MessageBox.Show("Not added\n ERROR: " +ex.Message );
                }
                
               
            }
            else { MessageBox.Show("please fill the empty filds"); }
        }
    }
}
