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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        IBL bl = BLFactory.GetBL(1);
        BO.User ThisUser;
        TimeSpan Time;
        int Second;
        public UserWindow(TimeSpan time, int second, BO.User User)
        {
            InitializeComponent();
            Time = time;
            Second = second;
            ThisUser = User;
            ClockTBO.Text = time.ToString("g");
            RefreshList(StationsList);
            bl.StartSimulator(Time, Second, UpdateTime);
        }
        public void RefreshList(ListView list)
        {
            try
            {
                list.ItemsSource = bl.GetAllStations();
            }catch(BO.BadStationIdException ex)
            {
                if(ex.ID == 0)
                {
                    list.ItemsSource = null;
                    list.Items.Refresh();
                }
                MessageBox.Show(ex.Message);
                return;
            }
            

            list.Items.Refresh();

        }

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

        private void UpdateTime(TimeSpan time)
        {
            ClockTBO.Text = time.ToString("g");
            Time = time;
        }
    }
}

