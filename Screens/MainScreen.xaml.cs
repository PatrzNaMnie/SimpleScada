using SimpleScada.Screens.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        public static Login loginScreen;
        public static AdminPanel adminPanel;

        private Timer _timer;
        public MainScreen()
        {
            InitializeComponent();


            _timer = new Timer(250); //Updates every quarter second.
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Enabled = true;


            
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            
            Dispatcher.Invoke(new Action(() => { labelLevel.Content = Login.securityLevel.ToString(); ; }));
            if (Login.securityLevel == 3)
            {
                Dispatcher.Invoke(new Action(() => { adminPanelButton.Visibility = Visibility.Visible; ; }));
                
            }
            else
            {
                Dispatcher.Invoke(new Action(() => { adminPanelButton.Visibility = Visibility.Hidden; ; }));
            }
        }


        private void Home_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Home();
        }

        private void Trend_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new SimpleScada.Screens.Views.Trend();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            _timer.Enabled = false;
            MainWindow.mainScreen.Close();
           App.Current.MainWindow.Show();
           
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            loginScreen = new Login();
            loginScreen.Show();
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            Login.logOut();
        }

        private void adminPanel_Click(object sender, RoutedEventArgs e)
        {
            adminPanel = new AdminPanel();
            adminPanel.Show();
        }
    }
}
