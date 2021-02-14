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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        IBL bl = BLFactory.GetBL(1);
        BO.User ThisUser; //Logged User
        public ManagerWindow(BO.User thisUser)
        {
            InitializeComponent();

            StartDataUpdate(BusesList);
            StartDataUpdate(LinesList);
            StartDataUpdate(StationsList);

            //update logged User
            ThisUser = thisUser;
            UserName.Text = " Hello " + ThisUser.UserName +  "!";
            
            this.Show();
        }
        #region List Managment

        public void StartDataUpdate(ListView list)
        {
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    if (list == BusesList)
                    {
                        list.ItemsSource = bl.GetAllBuses();
                    }
                    if (list == LinesList)
                    {
                        list.ItemsSource = bl.GetAllLines();
                    }
                    if (list == StationsList)
                    {
                        list.ItemsSource = bl.GetAllStations();
                    }
                }
                catch (BO.BadStationIdException ex)
                {
                    if(ex.ID == 0)
                    {
                        list.ItemsSource = null;
                    }
                }
                catch (BO.BadBusIdException ex)
                {
                    if (ex.ID == 0)
                    {
                        list.ItemsSource = null;
                    }
                }
                catch (BO.BadLineIdException ex)
                {
                    if (ex.ID == 0)
                    {
                        list.ItemsSource = null;
                    }
                }


                list.Items.Refresh();
            });

        }
        public void RefreshList(ListView list)
        {
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    if (list == BusesList)
                    {
                        list.ItemsSource = bl.GetAllBuses();
                    }
                    if (list == LinesList)
                    {
                        list.ItemsSource = bl.GetAllLines();
                    }
                    if (list == StationsList)
                    {
                        list.ItemsSource = bl.GetAllStations();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    list.ItemsSource = null;

                }
                
                                
                list.Items.Refresh();
            });
            
        }

        #endregion

        #region Buses

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            addBusWindow win = new addBusWindow(this);
            win.Show();
        }

        private void RemoveBus_Click(object sender, RoutedEventArgs e)
        {
            RemoveObject win = new RemoveObject("Bus", this);
            win.Show();
        }

        private void ShowBusInfo(object sender, MouseEventArgs e)
        {

            BO.BUS Bus = BusesList.SelectedItem as BO.BUS;
            ShowInfo win = new ShowInfo(Bus, this);

            win.Show();



        }
        #endregion

        #region Lines
        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            addLineWindow win = new addLineWindow(this);
            win.Show();

        }
        private void RemoveLine_Click(object sender, RoutedEventArgs e)
        {
            RemoveObject win = new RemoveObject("Line",this);
            win.Show();

        }
        private void ShowLineInfo(object sender, MouseEventArgs e)
        {

            BO.Line line = LinesList.SelectedItem as BO.Line;
            ShowInfo win = new ShowInfo(line, this);
            win.Show();
        }
        #endregion

        #region Stations
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddStationWindow win = new AddStationWindow(this);
            win.Show();
        }

        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {
            RemoveObject win = new RemoveObject("Station", this);
            win.Show();
        }

        private void ShowStationInfo(object sender, MouseEventArgs e)
        {

            BO.Station stat = StationsList.SelectedItem as BO.Station;
            ShowInfo win = new ShowInfo(stat, this);
            win.Show();
        }
        #endregion

        private void LogOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            ThisUser = null;
            this.Close();
            win.Show();
        }

        private void UserDetails_Click(object sender, RoutedEventArgs e)
        {
            ShowInfo win = new ShowInfo(ThisUser, this);
            win.Show();
        }

    }
}
