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

namespace targil3B
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddBusWin : Window
    {
       
        public AddBusWin()
        {
            InitializeComponent();
            DP.SelectedDate = DateTime.Now;
            //DP.BlackoutDates.AddDatesInPast();
        }

        private void addbus1_Click(object sender, RoutedEventArgs e)
        {
            DateTime SDate = DP.SelectedDate.Value.Date;
            if (SDate > DateTime.Now)
            {
                MessageBox.Show("ERROR: you cant add an bus in the future");
            }
            string lisence = lisence_num.Text;
            int Ilisence = new int();
            bool flag = int.TryParse(lisence, out Ilisence);
            if(!flag)
            {
                MessageBox.Show("ERROR: the lisence number isn't correct");
            }
            BUS nbus = new BUS();

        }
    }
}
