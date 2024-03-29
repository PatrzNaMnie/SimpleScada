﻿
using Ganss.Excel;
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

        public static List<Variables> variables = new List<Variables>();
        private ReadVariables rV = new ReadVariables();
        private List<AlarmList> alarmInList = new List<AlarmList>();

        public static List<WriteBoolData> writeBoolDataList = new List<WriteBoolData>();
        public static List<WriteRealData> writeRealDataList = new List<WriteRealData>();

        public MainScreen()
        {
            InitializeComponent();

            DataContext = new Logo();

            // Read variables from Variables.xlsx file (Excel/Vairables.xlsl)
            variables.AddRange(rV.readVar());


            _timer1 = new Timer(1000); //Updates every half second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;

            _timer2 = new Timer(250); //Updates every quarter second.
            _timer2.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
            _timer2.Enabled = true;

            MainWindow.plcConnect.setVariables(variables);

            // Send variables to collection - need to fix !!
            foreach (var item in MainScreen.variables)
            {
                if (item.MeasuringUnit.Equals("Send") && item.Type.Equals("BOOL"))
                    writeBoolDataList.Add(new WriteBoolData() { Name = item.Name, Address = item.Source, Value = false });
                else if (item.MeasuringUnit.Equals("Send") && item.Type.Equals("REAL"))
                    writeRealDataList.Add(new WriteRealData() { Name = item.Name, Address = item.Source, Value = 0 });
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            actualTime = DateTime.Now;

            // Top bar info labels
            if (Login.operatorName == null)
            {
                Dispatcher.Invoke(new Action(() => { usernameLabel.Content = "Spectactor"; ; }));
            }
            else
            {
                Dispatcher.Invoke(new Action(() => { usernameLabel.Content = Login.operatorName; ; }));
            }
            Dispatcher.Invoke(new Action(() => { dateAndTimeLabel.Content = actualTime; ; }));

            if (Login.securityLevel == 3)
            {
                Dispatcher.Invoke(new Action(() => { adminPanelButton.IsEnabled = true; ; }));

            }
            else
            {
                Dispatcher.Invoke(new Action(() => { adminPanelButton.IsEnabled = false; ; }));
            }

            // Data monitor
            MainWindow.plcConnect.dataMonitor(actualTime, variables);

            // Send data
            MainWindow.plcConnect.sendBoolData(writeBoolDataList);
            MainWindow.plcConnect.sendRealData(writeRealDataList);

            // Alarm Handling 
            Dispatcher.Invoke(new Action(() => { alarmList.ItemsSource = null; }));
            Dispatcher.Invoke(new Action(() => { alarmList.ItemsSource = MainWindow.plcConnect.getAlarmList(); }));

            foreach (var item in writeBoolDataList)
            {
                item.Value = false;
            }

        }

        private void OnTimedEvent2(object source, ElapsedEventArgs e)
        {

            

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
            Views.Home.stopTimer();
            MainWindow.plcConnect.turnOffDataCollection();
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

        private void Alarm_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Alarm();
        }

        private void AlarmHistory_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new HistoryAlarm();
        }
    }
}
