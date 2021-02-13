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
    /// Interaction logic for ShowBusInfo.xaml
    /// </summary>
    public partial class ShowInfo : Window
    {
        readonly int defulth = 270;
        ManagerWindow Main;
        object ThisObj;
        Type ThisType;
        IBL bl = BLFactory.GetBL(1);
        List<BO.Station> stations;
        public ShowInfo(BO.BUS bus, ManagerWindow main)
        {
            InitializeComponent();
            Main = main;
            ThisObj = bus;
            ThisType = typeof(BO.BUS);

            ClearNums();
            number1pre.Text = "License Number:";
            number1data.Text = bus.LicenseNum.ToString();
            number2pre.Text = "Start Date:";
            number2data.Text = bus.pSD;
            number3pre.Text = "Last Repair:";
            number3data.Text = bus.pLR;
            number4pre.Text = "Overall KM:";
            number4data.Text = bus.TotalTrip.ToString();
            number5pre.Text = "KM since Repair:";
            number5data.Text = bus.ckm.ToString();
            number6pre.Text = "Gaz Amount:";
            number6data.Text = bus.FuelRemain.ToString();
            AddStationB.Opacity = 0;
            AddStationB.IsEnabled = false;
            RemoveStationB.Opacity = 0;
            RemoveStationB.IsEnabled = false;
            UpdateStationB.Opacity = 0;
            UpdateStationB.IsEnabled = false;
        }
        public ShowInfo(BO.Line line, ManagerWindow main)
        {
            InitializeComponent();
            Main = main;
            ThisType = typeof(BO.Line);
            ThisObj = line;

            ClearNums();
            this.Height = 640;
            List<BO.LineStation> lineStations = (from station in bl.GetAllLineStations()
                                                where station.LineID == line.ID 
                                                select station).ToList();
            stations = (from station in lineStations
                                         select bl.GetAllStations().ToList().Find(p => p.Code == station.Station)).ToList();
            LineStationView.ItemsSource = stations;
            RemoveObj.Content = "Remove Line";
            UpdateObj.Content = "Update Line";
            number1pre.Text = "ID:";
            number1data.Text = line.ID.ToString();
            number2pre.Text = "Code:";
            number2data.Text = line.Code.ToString();
            number3pre.Text = "Area:";
            number3data.Text = line.Area.ToString();
            
            number6pre.Text = "First Station:";
            number6data.Text = line.FirstStation.ToString();
            number7pre.Text = "Last Station:";
            number7data.Text = line.LastStation.ToString();
        }

        public ShowInfo(BO.Station Station, ManagerWindow main)
        {
            InitializeComponent();
            Main = main;
            ThisType = typeof(BO.Station);
            ThisObj = Station;

            ClearNums();
            number1pre.Text = "Code:";
            number1data.Text = Station.Code.ToString();
            number2pre.Text = "Name:";
            number2data.Text = Station.Name;
            number3pre.Text = "Longitude:";
            number3data.Text = Station.Longitude.ToString();
            number4pre.Text = "Lattitude:";
            number4data.Text = Station.Latitude.ToString();
           
        }


            private void ClearNums()
        {
            number1pre.Text = "";
            number1data.Text = "";
            number2pre.Text = "";
            number2data.Text = "";
            number3pre.Text = "";
            number3data.Text = "";
            number4pre.Text = "";
            number4data.Text = "";
            number5pre.Text = "";
            number5data.Text = "";
            number6pre.Text = "";
            number6data.Text = "";
            number7pre.Text = "";
            number7data.Text = "";
            number8pre.Text = "";
            number8data.Text = "";
            Height = defulth;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if(ThisType == typeof(BO.BUS))
            {
                SendToBusWin();
                return;
            }
            if (ThisType == typeof(BO.Line))
            {
                SendToLineWin();
                return;
            }
            if (ThisType == typeof(BO.Station))
            {
                SendToStationWin();
                return;
            }


        }
        private void SendToBusWin()
        {
            BO.BUS newbus = (BO.BUS)ThisObj;
            addBusWindow win = new addBusWindow(newbus.LicenseNum, Main);
            win.Show();
            this.Close();
        }
        private void SendToLineWin()
        {
            BO.Line newline = (BO.Line)ThisObj;
            addLineWindow win = new addLineWindow(newline.ID, Main);
            win.Show();
            this.Close();
        }
        private void SendToStationWin()
        {
            BO.Station newStation = (BO.Station)ThisObj;
            AddStationWindow win = new AddStationWindow(newStation.Code, Main);
            win.Show();
            this.Close();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ThisType == typeof(BO.BUS))
                {
                    bl.RemoveBus((ThisObj as BO.BUS).LicenseNum);
                    this.Close();
                    Main.RefreshList(Main.BusesList);
                } else
                if (ThisType == typeof(BO.Line))
                {
                    bl.RemoveLine((ThisObj as BO.Line).ID);
                    this.Close();
                    Main.RefreshList(Main.LinesList);
                } else
                if (ThisType == typeof(BO.Station))
                {
                    bl.RemoveStation((ThisObj as BO.Station).Code);
                    this.Close();
                    Main.RefreshList(Main.StationsList);
                }
                else throw new Exception("No Matching Function");
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RemoveStationB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteLineStation((LineStationView.SelectedItem as BO.Station).Code, (ThisObj as BO.Line).ID);
                BO.Station temp = stations.Find(p => p.Code == (LineStationView.SelectedItem as BO.Station).Code);
                stations.Remove(temp);
                LineStationView.Items.Refresh();
            }
            catch (BO.BadStationIdException ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void UpdateStationB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddStationB_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
