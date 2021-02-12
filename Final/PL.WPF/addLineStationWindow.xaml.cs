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
    /// Interaction logic for addLineStationWindow.xaml
    /// </summary>
    public partial class addLineStationWindow : Window
    {
        int LineId;
        int StationIndex;
        int PrevStation;
        int NextStation;
        int StationId;
        IBL bl = BLFactory.GetBL(1);
        ShowInfo SI;
        public addLineStationWindow(BO.LineStation station, ShowInfo Si)
        {
            InitializeComponent();

            SI = Si;
            AddButton.Opacity = 0;
            AddButton.IsEnabled = false;
            StationIDBO.Text = station.Station.ToString();
            StationIDBO.IsEnabled = false;
            LineIDTBO.Text = station.LineID.ToString();
            LineIDTBO.IsEnabled = false;
        }
        public addLineStationWindow()
        {
            InitializeComponent();
            UpdateButton.Opacity = 0;
            UpdateButton.IsEnabled = false;
        }


        private void StationIDBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(StationIDBO.Text, out StationId))
            {
                MessageBox.Show("numbers only!");
            }
        }
        private void LineIDTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(LineIDTBO.Text, out LineId))
            {
                MessageBox.Show("numbers only!");
            }
        }

        private void StationIndexTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(StationIndexTBO.Text, out StationIndex))
            {
                MessageBox.Show("numbers only!");
            }
        }

        private void PrevStationTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(PrevStationTBO.Text, out PrevStation))
            {
                MessageBox.Show("numbers only!");
            }
        }

        private void NextStationTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(NextStationTBO.Text, out NextStation))
            {
                MessageBox.Show("numbers only!");
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (bl.GetStaion(StationId)!= null)
            {
                try
                {
                    bl.AddLineStation(new BO.LineStation()
                    {
                        LineID = LineId,
                        LineStationIndex = StationIndex,
                        NextStation = NextStation,
                        PrevStation = PrevStation,
                        Station = StationId
                    });
                }
                catch (BO.BadStationIdException ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddLineStation(new BO.LineStation()
                {
                    LineID = LineId,
                    LineStationIndex = StationIndex,
                    NextStation = NextStation,
                    PrevStation = PrevStation,
                    Station = StationId
                });

            }
            catch (BO.BadStationIdException ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

    }
}
