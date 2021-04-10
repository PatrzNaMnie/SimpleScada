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

namespace SimpleScada.Screens
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static int securityLevel { get; private set; }
        public static string operatorName { get; private set; }
        public Login()
        {
            InitializeComponent();

            securityLevel = 0;
            operatorName = "Spectactor";
        }


        private void logOn_Click(object sender, RoutedEventArgs e)
        {
            using(var db = new SimpleScadaContext())
            {
                var users = db.Users.OrderBy(p => p.Id);
                int countUsers = 0;
                passwordInfo.Content = "";
                usernameInfo.Content = "";
                foreach (var user in users)
                {
                    if(user.User.Equals(txtUsername.Text) && user.Password.Equals(txtPassword.Text))
                    {
                        countUsers++;
                        securityLevel = user.Level;
                        operatorName = user.User;
                        //MessageBox.Show("Successfull");
                        MainScreen.loginScreen.Close();
                    }
                    if(user.User.Equals(txtUsername.Text) && !user.Password.Equals(txtPassword.Text))
                    {
                        countUsers++;
                        passwordInfo.Content = "Wrong password!";
                    }
                    
                }

                if(countUsers == 0)
                {
                    usernameInfo.Content = $"User doesn't exist in database.";
                }
            }
        }

        public static void logOut()
        {
            securityLevel = 0;
            operatorName = "Spectactor";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainScreen.loginScreen.Close();
        }

    }
}
