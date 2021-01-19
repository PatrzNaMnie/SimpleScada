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

        private Timer _timer1;
        private Timer _timer2;
        public MainScreen()
        {
            InitializeComponent();


            _timer1 = new Timer(500); //Updates every quarter second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;

            _timer2 = new Timer(250); //Updates every quarter second.
            _timer2.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
            _timer2.Enabled = true;


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
        }

        private void OnTimedEvent2(object source, ElapsedEventArgs e)
        {

                // Alarm Handling 

                using (var db = new SimpleScadaContext())
                {
                    var variables = db.Variables.OrderBy(p => p.Id);

                    // do tego trzeba by zrobić osobną metodę albo klasę 

                    foreach (var variable in variables)
                    {
                        if(variable.Alarm == true)
                        {
                            var alarmInList = db.AlarmList.Where(p => p.VariableName.Equals(variable.Name)) as AlarmList;

                            if (variable.MeasuringUnit.Equals("None") && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("True"))
                            {
                             
                                if (alarmInList.Active==null)
                                {
                                    db.AlarmList.Add(new AlarmList() { TimeReceived = actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });
                                    
                                }
                            }
                            /*else if(variable.MeasuringUnit.Equals("None") && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("False"))
                            {
                                if (alarmInList.Active == true)
                                {
                                db.AlarmHistory.Add(new AlarmHistory() { TimeReceived = alarmInList.TimeReceived, TimeAcknowledge = actualTime.ToLongTimeString(), 
                                        VariableName = alarmInList.VariableName, Text = alarmInList.Text, AlarmValue = alarmInList.AlarmValue });

                                db.AlarmList.Remove(alarmInList);
                                
                                }
                            }*/
                        }
                    }
                db.Database.GetType();
                db.SaveChanges();


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
            _timer1.Enabled = false;
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
