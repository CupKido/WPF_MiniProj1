using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BL;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Thickness marginFirstButton = new Thickness(10.6, 110, 0, 0);
        readonly Thickness marginSecondButton = new Thickness(11.6, 166, 0, 0);
        readonly Thickness marginThirdButton = new Thickness(10.6, 222, 0, 0);
        readonly Thickness marginForthButton = new Thickness(11.6, 278, 0, 0);
        readonly Thickness marginOut = new Thickness(-300, 0, 0, 0);
        IBL bl = BLFactory.GetBL(1);
        ObservableCollection<BO.BUS> ObserListOfBuses = new ObservableCollection<BO.BUS>();
        public MainWindow()
        {
            
            InitializeComponent();

            try
            {
                foreach (var item in bl.GetAllBuses())
                {
                    ObserListOfBuses.Add(item);
                }
            }
            catch
            {

            }
            changeToMainMenu();
            this.Show();
        }
        void disappearButton(Button butt)
        {
            butt.Opacity = 0;
            butt.IsEnabled = false;
            butt.Margin = marginOut;
            butt.Width = 0;
            butt.Height = 0;
        }

        void showButton(Button butt, Thickness marg)
        {
            butt.Opacity = 100;
            butt.IsEnabled = true;
            butt.Margin = marg;
            butt.Width = 118;
            butt.Height = 51;
        }

        void changeToMainMenu()
        {
            showButton(Lines, marginFirstButton);
            showButton(Stations, marginSecondButton);
            showButton(Buses, marginThirdButton);
            disappearButton(addBus);
            disappearButton(removeBus);
            disappearButton(updateBus);
            disappearButton(addLine);
            disappearButton(backToMain);
        }

        void changeToBusMenu()
        {
            showButton(addBus, marginFirstButton);
            showButton(removeBus, marginSecondButton);
            showButton(updateBus, marginThirdButton);
            showButton(backToMain, marginForthButton);
            disappearButton(Lines);
            disappearButton(Stations);
            disappearButton(Buses);
        }
        void changeToLineMenu()
        {
            disappearButton(Lines);
            disappearButton(Stations);
            disappearButton(Buses);
            showButton(addLine, marginFirstButton);
            showButton(backToMain, marginForthButton);
        }

        public void Click_OpenBuses(object sender, RoutedEventArgs e)
        {
            //Buses buses = new Buses();
            //buses.Show();
            currentTitle.Text = "Buses";
            ((GridView)subjectsList.View).Columns[0].Header = "bus Number";
            ((GridView)subjectsList.View).Columns[0].Width = 80;
            try
            {
                subjectsList.ItemsSource = bl.GetAllBuses();

            }
            catch
            {
                MessageBox.Show("No Buses In DataSource!");
            }
            
            changeToBusMenu();
        }
        public void Click_OpenLines(object sender, RoutedEventArgs e)
        {
            //Lines lines = new Lines(bl);
            //lines.Show();
            currentTitle.Text = "Lines";
            ((GridView)subjectsList.View).Columns[0].Header = "line Number";
            ((GridView)subjectsList.View).Columns[0].Width = 90;
            try
            {
                subjectsList.ItemsSource = bl.GetAllLines();
            }
            catch(Exception ex)
            {
                MessageBox.Show("No Lines In DataSource!" + ex.ToString());
            }
            changeToLineMenu();

        }
        public void Click_OpenStations(object sender, RoutedEventArgs e)
        {
            currentTitle.Text = "Stations";
            ((GridView)subjectsList.View).Columns[0].Header = "line Number";
            ((GridView)subjectsList.View).Columns[0].Width = 90;
            try
            {
                subjectsList.ItemsSource = bl.GetAllLines();
            }
            catch
            {
                MessageBox.Show("No Stations In DataSource!");
            }
        }

        private void subjectsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void LinesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void addBus_Click(object sender, RoutedEventArgs e)
        {
            addBusWindow win = new addBusWindow(bl);
            win.Show();
        }

        private void backToMain_Click(object sender, RoutedEventArgs e)
        {
            changeToMainMenu();
        }

        private void removeBus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateBus_Click(object sender, RoutedEventArgs e)
        {

        }
        private void addLine_Click(object sender, RoutedEventArgs e)
        {
            addLineWindow win = new addLineWindow(bl,this);
            win.Show();
        }
    }



}
