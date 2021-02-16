using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

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
        BackgroundWorker simulatorWorker;
        BackgroundWorker stationSimWorker;
        public UserWindow(TimeSpan time, int second, BO.User User)
        {
            InitializeComponent();
            Time = time;
            Second = second;
            ThisUser = User;
            ClockTBO.Text = time.ToString("g");
            RefreshList(StationsList);
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

        private void UpdateTime_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ClockTBO.Text = ((TimeSpan)e.UserState).ToString("g");
        }

        private void SimClockButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan startTime = TimeSpan.Parse(ClockTBO.Text);
            if (SimClockButton.Content.ToString() == "Start Simulator")
            {
                
                if (Second != 0)
                {
                    


                    // Activate Clock
                    simulatorWorker = new BackgroundWorker
                    {
                        WorkerSupportsCancellation = true,
                        WorkerReportsProgress = true
                    };
                    simulatorWorker.ProgressChanged += UpdateTime_ProgressChanged;
                    simulatorWorker.DoWork += SimulatorWorker_DoWork;
                    simulatorWorker.RunWorkerAsync(new object[] { startTime, Second });
                    SimClockButton.Content = "Stop Simulator";
                    //// Activate station view worker
                    //if (simulatorWorker == null)
                    //    InitStationSimWorker();
                    //if (stationSimWorker.IsBusy && stationSimWorker.CancellationPending)
                    //    while (stationSimWorker.IsBusy) // wait for station sim worker to be stopped
                    //        Thread.Sleep(50);
                    //stationSimWorker.RunWorkerAsync();
                }
                else if (Second <= 1000)
                    MessageBox.Show("Invalid time or rate value!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show("rate can't be more than 1000", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else // Stop Clock
            {
                
                
                // Stop the Clock
                bl.StopSimulator();
                if (simulatorWorker.WorkerSupportsCancellation == true)
                    simulatorWorker.CancelAsync();
                SimClockButton.Content = "Start Simulator";
                //// stop station simulation
                //if (stationSimWorker.WorkerSupportsCancellation == true)
                //    stationSimWorker.CancelAsync();
            }
        }

        void SimulatorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] Data= e.Argument as object[];
            bl.StartSimulator((TimeSpan)Data[0], (int)Data[1], (TimeSpan x) =>
            {
                if (!simulatorWorker.CancellationPending)
                {
                    simulatorWorker.ReportProgress(0, x);
                }
                    
            });
            while (!simulatorWorker.CancellationPending)
            {
                Thread.Sleep(1000);
            }

        }
    }
}

