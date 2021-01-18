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
        DateTime actualTime;

        private Timer _timer;
        public MainScreen()
        {
            InitializeComponent();


            _timer = new Timer(100); //Updates every quarter second.
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Enabled = true;


            
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            actualTime = DateTime.Now;
            Dispatcher.Invoke(new Action(() => { usernameLabel.Content = Login.operatorName; ; }));
            Dispatcher.Invoke(new Action(() => { dateAndTimeLabel.Content = actualTime; ; }));
            
            if (Login.securityLevel == 3)
            {
                Dispatcher.Invoke(new Action(() => { adminPanelButton.IsEnabled = true; ; }));
                
            }
            else
            {
                Dispatcher.Invoke(new Action(() => { adminPanelButton.IsEnabled = false; ; }));
            }

            // Alarm Handling 

            using (var db = new SimpleScadaContext())
            {
                var variables = db.Variables.OrderBy(p => p.Id);

                foreach (var variable in variables)
                {
                    if(variable.Alarm == true)
                    {
                        AlarmList alarmInList = (AlarmList)db.AlarmList.Where(p => p.VariableName.Equals(variable.Name));
                        if (variable.MeasuringUnit.Equals("None") && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("True"))
                        {
                            if (alarmInList == null)
                            {
                                db.AlarmList.Add(new AlarmList() { TimeReceived = actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });
                            }
                        }
                        else if(variable.MeasuringUnit.Equals("None") && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("False"))
                        {
                            if(alarmInList != null)
                            {
                                db.AlarmHistory.Add(new AlarmHistory() { TimeReceived = alarmInList.TimeReceived, TimeAcknowledge = actualTime.ToLongTimeString(), 
                                    VariableName = alarmInList.VariableName, Text = alarmInList.Text, AlarmValue = alarmInList.AlarmValue });

                                db.AlarmList.Remove((AlarmList)alarmInList);
                            }
                        }
                    }
                }
                
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
