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
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL(1);
        BO.User ThisUser = new BO.User { UserName = "", Password = "", Admin = false };
        bool UpdateMode = false;
        public MainWindow()
        {
            InitializeComponent();

            //------To Remove-------
            UserNameTBO.Text = "Admin";
            PasswordTBO.Password = "Admin";
            //----------------------

            SignUpButton.IsEnabled = false;
            SignUpButton.Opacity = 0;


            this.Show();
        }

        public MainWindow(string UserName, string Password)
        {
            InitializeComponent();


            this.Height = 250;
            SignUpTBL.IsEnabled = false;
            SignUpTBL.Opacity = 0;
            ForgotPasswordTBL.IsEnabled = false;
            ForgotPasswordTBL.Opacity = 0;

            ConfirmPasswordTBL.IsEnabled = true;
            ConfirmPasswordTBL.Opacity = 1;
            ConfirmPasswordTBO.IsEnabled = true;
            ConfirmPasswordTBO.Opacity = 1;

            LoginButton.IsEnabled = false;
            LoginButton.Opacity = 0;

            //------To Remove-------
            UserNameTBO.Text = UserName;
            PasswordTBO.Password = Password;
            //----------------------

            this.Show();
        }

        public MainWindow(BO.User user)
        {
            InitializeComponent();

            UpdateMode = true;
            ThisUser = user;
            this.Height = 250;
            SignUpTBL.IsEnabled = false;
            SignUpTBL.Opacity = 0;
            ForgotPasswordTBL.IsEnabled = false;
            ForgotPasswordTBL.Opacity = 0;

            ConfirmPasswordTBL.IsEnabled = true;
            ConfirmPasswordTBL.Opacity = 1;
            ConfirmPasswordTBO.IsEnabled = true;
            ConfirmPasswordTBO.Opacity = 1;
            UserNameTBO.IsEnabled = false;

            LoginButton.IsEnabled = false;
            LoginButton.Opacity = 0;

            SignUpButton.Content = "Update";

            //------To Remove-------
            UserNameTBO.Text = user.UserName;
            PasswordTBO.Password = user.Password;
            //----------------------

            this.Show();
        }

        private void SignUp_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow win = new MainWindow(UserNameTBO.Text, PasswordTBO.Password);
            this.Close();
            win.Show();

        }
        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("No Such Function");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ThisUser.UserName = UserNameTBO.Text;
            ThisUser.Password = PasswordTBO.Password;
            try
            {
                ThisUser = bl.GetUser(ThisUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if(ThisUser.Admin)
            {
            ManagerWindow win = new ManagerWindow(ThisUser);
            win.Show();
            this.Close();
            return;
            }
            //UserWindow win = new UserWindow(ThisUser);
            //win.Show();
            this.Close();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateMode)
            {
                UpdateButton_Click();
                return;
            }
            if (PasswordTBO.Password != ConfirmPasswordTBO.Password)
            {
                MessageBox.Show("Passwords Dont Match!");
                return;
            }
            try
            {
                ThisUser.UserName = UserNameTBO.Text;
                ThisUser.Password = PasswordTBO.Password;
                bl.AddUser(ThisUser);
            }
            catch (BO.BadUserNameException ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ID);
                return;
            }

            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void UpdateButton_Click()
        {
            
            if (PasswordTBO.Password != ConfirmPasswordTBO.Password)
            {
                MessageBox.Show("Passwords Dont Match!");
                return;
            }
            try
            {
                ThisUser.Password = PasswordTBO.Password;
                bl.UpdateUser(ThisUser);
            }catch(BO.BadUserNameException ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ID);
                return;
            }
            this.Close();
        }
    }
}
