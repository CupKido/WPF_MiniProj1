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
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        IBL bl = BLFactory.GetBL(1);
        BO.User ThisUser;
        public UsersWindow()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("No Such Function");
        }
        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("No Such Function");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ThisUser = bl.GetUser(UserNameTBO.Text)
                
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if(ThisUser.Password != PasswordTBO.Text)
            {
                MessageBox.Show("Incorrect Password Or UserName");
                return;
            }
            if(ThisUser.Admin)
            {
                //ManagerWindow win = new ManagerWindow(ThisUser);
                //win.Show();
                this.Close();
                return;
            }
            //UserWindow win = new UserWindow(ThisUser);
            //win.Show();
            this.Close();
        }
    }
}
