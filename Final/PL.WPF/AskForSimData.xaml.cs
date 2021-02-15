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

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for AskForData.xaml
    /// </summary>
    public partial class AskForSimData : Window
    {
        BO.User ThisUser;
        public AskForSimData(BO.User User)
        {
            InitializeComponent();
            ThisUser = User;
            this.Show();
        }

        public void AskDataButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserWindow win = new UserWindow(TimeSpan.Parse(AskDataTBO.Text), int.Parse(AskSecondTBO.Text), ThisUser);
                win.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
