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
        UserWindow UserMain;
        public event Action<TimeSpan, int> dataEntered;
        public AskForSimData(UserWindow usermain)
        {
            InitializeComponent();
            UserMain = usermain;
            dataEntered += UserMain.updateFromAsk;
            this.Show();
        }

        public void AskDataButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataEntered.Invoke(TimeSpan.Parse(AskDataTBO.Text), int.Parse(AskSecondTBO.Text));
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
