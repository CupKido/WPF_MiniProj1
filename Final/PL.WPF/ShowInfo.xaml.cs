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
        MainWindow Main;
        object ThisObj;
        Type ThisType;
        public ShowInfo(BO.BUS bus, MainWindow main)
        {
            InitializeComponent();
            Main = main;
            ThisObj = bus;
            ThisType = typeof(BO.BUS);
            number1pre.Text = "License Number:";
            number1data.Text = bus.LicenseNum.ToString();
            number2pre.Text = "Start Date:";
            number2data.Text = bus.pSD;
        }
        public ShowInfo(BO.Line line, MainWindow main)
        {
            InitializeComponent();
            Main = main;
            ThisType = typeof(BO.Line);
            ThisObj = line;
        }

        private void Update(object sender, RoutedEventArgs e)
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
            if (ThisType == typeof(BO.Line))
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
            BO.BUS newbus = (BO.BUS)ThisObj;
            addBusWindow win = new addBusWindow(newbus.LicenseNum, Main);
            win.Show();
            this.Close();
        }
    }
}
