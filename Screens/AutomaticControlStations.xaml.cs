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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleScada.Screens
{
    /// <summary>
    /// Interaction logic for AutomaticControlStations.xaml
    /// </summary>
    public partial class AutomaticControlStations : Window
    {
        private string Name { get; set; }

        private string setpointName { get; set; }
        private string secondSetpointName { get; set; }

        private StateControl stateControl = new StateControl();

        private static System.Timers.Timer _timer1;

        public AutomaticControlStations(string Name)
        {
            InitializeComponent();

            StationName.Text = Name;
            this.Name = Name;

            _timer1 = new System.Timers.Timer(500); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;

            Sp1.Visibility = Visibility.Hidden;

            Sp2.Visibility = Visibility.Hidden;
        }

        public AutomaticControlStations(string Name, string setpointName)
        {
            InitializeComponent();

            this.setpointName = setpointName;

            StationName.Text = Name;
            this.Name = Name;

            _timer1 = new System.Timers.Timer(500); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;

            Sp1Name.Text = setpointName;
            Sp1.Visibility = Visibility.Visible;

            Sp2Name.Text = secondSetpointName;
            Sp2.Visibility = Visibility.Hidden;
        }

        public AutomaticControlStations(string Name, string firstSetpointName, string secondSetpointName)
        {
            InitializeComponent();

            this.setpointName = firstSetpointName;
            this.secondSetpointName = secondSetpointName;

            StationName.Text = Name;
            this.Name = Name;

            _timer1 = new System.Timers.Timer(500); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;

            Sp1Name.Text = firstSetpointName;
            Sp1.Visibility = Visibility.Visible;

            Sp2Name.Text = secondSetpointName;
            Sp2.Visibility = Visibility.Visible;
        }


        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // Read process status
            Dispatcher.Invoke(new Action(() => { Status.Text = stateControl.chooseProgramState(Name, MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_STATE")).Value); }));


        }

        private void startClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_START");
        }

        private void stopClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_STOP");
        }

        private void autoClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_AUTO");
        }

        private void manualClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_MANUAL");
        }

        private void sendSignal(string operation)
        {

            MainScreen.writeBoolDataList.Find(p => p.Name.Equals(Name + operation)).Value = true;
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void sendSP1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {

                double value = new double();
                Dispatcher.Invoke(new Action(() => { value = double.Parse(Sp1.Text, System.Globalization.CultureInfo.InvariantCulture); }));
                MainScreen.writeRealDataList.Find(p => p.Name.Equals(Name + "_" + setpointName)).Value = value;
            }
        }

        private void sendSP2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {

                double value = new double();
                Dispatcher.Invoke(new Action(() => { value = double.Parse(Sp2.Text, System.Globalization.CultureInfo.InvariantCulture); }));
                MainScreen.writeRealDataList.Find(p => p.Name.Equals(Name + "_" + secondSetpointName)).Value = value;
            }
        }
    }
}
