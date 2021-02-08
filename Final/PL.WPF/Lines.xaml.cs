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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Lines : LinesWindow
    {
        IBL BL;
        public Lines(IBL newBL)
        {
            InitializeComponent();

            BL = newBL;
            try
            {
                LinesList.ItemsSource = BL.GetAllLines();
            }
            catch
            {
                MessageBox.Show("No Lines In DataSource!");
            }
        }
    }
}
