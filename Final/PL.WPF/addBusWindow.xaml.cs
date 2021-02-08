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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class addBusWindow : Window
    {
        int temp = new int();
        public addBusWindow()
        {
            InitializeComponent();
        }

        private void licenseTBO_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(licenseTBO.Text, out temp))
            {
                MessageBox.Show("numbers only!");
            }
        }
        private void licenseTBO_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
