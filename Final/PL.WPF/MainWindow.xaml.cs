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
            //to Show
            UserNameTBO.Visibility = Visibility.Visible;
            UserNameTBL.Visibility = Visibility.Visible;
            PasswordTBO.Visibility = Visibility.Visible;
            PasswordTBL.Visibility = Visibility.Visible;
            SignUpTBL.Visibility = Visibility.Visible;
            ForgotPasswordTBL.Visibility = Visibility.Visible;
            LoginButton.Visibility = Visibility.Visible;

            //to Hide
            SignUpButton.Visibility = Visibility.Collapsed;
            ConfirmPasswordTBL.Visibility = Visibility.Collapsed;
            ConfirmPasswordTBO.Visibility = Visibility.Collapsed;


            //------To Remove-------
            UserNameTBO.Text = "Admin";
            PasswordTBO.Password = "Admin";
            //----------------------

            this.Show();
        }

        public MainWindow(string UserName, string Password)
        {
            InitializeComponent();

            this.Height = 250;

            //to Show
            UserNameTBO.Visibility = Visibility.Visible;
            UserNameTBL.Visibility = Visibility.Visible;
            PasswordTBO.Visibility = Visibility.Visible;
            PasswordTBL.Visibility = Visibility.Visible;
            ConfirmPasswordTBL.Visibility = Visibility.Visible;
            ConfirmPasswordTBL.IsEnabled = true;
            ConfirmPasswordTBL.Opacity = 1;
            ConfirmPasswordTBO.Visibility = Visibility.Visible;
            ConfirmPasswordTBO.Background = Brushes.Red;
            ConfirmPasswordTBO.IsEnabled = true;
            ConfirmPasswordTBO.Opacity = 1;
            SignUpButton.Visibility = Visibility.Visible;

            //to Hide
            SignUpTBL.Visibility = Visibility.Collapsed;
            ForgotPasswordTBL.Visibility = Visibility.Collapsed;
            LoginButton.Visibility = Visibility.Collapsed;

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

            //to Show
            UserNameTBO.Visibility = Visibility.Visible;
            UserNameTBO.IsEnabled = false;
            UserNameTBL.Visibility = Visibility.Visible;
            PasswordTBO.Visibility = Visibility.Visible;
            PasswordTBL.Visibility = Visibility.Visible;
            ConfirmPasswordTBL.Visibility = Visibility.Visible;
            ConfirmPasswordTBL.IsEnabled = true;
            ConfirmPasswordTBL.Opacity = 1;
            ConfirmPasswordTBO.Visibility = Visibility.Visible;
            ConfirmPasswordTBO.Background = Brushes.Red;
            ConfirmPasswordTBO.IsEnabled = true;
            ConfirmPasswordTBO.Opacity = 1;
            SignUpButton.Visibility = Visibility.Visible;
            SignUpButton.Content = "Update";

            //to Hide
            LoginButton.Visibility = Visibility.Collapsed;
            ForgotPasswordTBL.Visibility = Visibility.Collapsed;
            SignUpTBL.Visibility = Visibility.Collapsed;
            
            

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

        private void ConfirmPassword_Changed(object sender, RoutedEventArgs e)
        {
            if (PasswordTBO.Password != ConfirmPasswordTBO.Password)
            {
                ConfirmPasswordTBO.Background = Brushes.Red;
                return;
            }
            ConfirmPasswordTBO.Background = Brushes.Green;
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
            StartSimulator();
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

        private void StartSimulator()
        {
            AskForSimData win = new AskForSimData(ThisUser);
            win.Show();
            this.Close();
        }
    }
}
