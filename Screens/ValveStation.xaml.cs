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
    /// Interaction logic for ValveStation.xaml
    /// </summary>
    public partial class ValveStation : Window
    {
        private static Timer _timer1;

        private StateControl stateControl = new StateControl();
        private string Name { get; set; }
        public ValveStation(string Name)
        {
            InitializeComponent();

            _timer1 = new Timer(250); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;

            stationName.Content = Name;
            this.Name = Name;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {


            var uriSource = new Uri(stateControl.setValveImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgUV.Source = new BitmapImage(uriSource); }));
            Dispatcher.Invoke(new Action(() => { txtStatus.Text = stateControl.setValveTxt(MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_STATE")).Value); }));
            Dispatcher.Invoke(new Action(() => { txtMode.Text = checkMode(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_MODE")).Value); }));
            Dispatcher.Invoke(new Action(() => { modeGraphic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_MODE")).Value); }));
            Dispatcher.Invoke(new Action(() => { faultGrapic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_FAULT")).Value); }));
            Dispatcher.Invoke(new Action(() => { blockadeGraphic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_BLOCKADE")).Value); }));

        }


        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }


        private void Manual(object sender, RoutedEventArgs e)
        {
            
            AutoManual.Content = "AUTO";
        }

        private void Auto(object sender, RoutedEventArgs e)
        {
            AutoManual.Content = "MANUAL";
        }

        private void Off(object sender, RoutedEventArgs e)
        {
            MainWindow.plcConnect.writeBoolValue(MainWindow.plcConnect.getVariables().Find(p => p.Name.Equals(Name + "_OPEN_CLOSE")).Source, true);
            OnOff.Content = "OFF";
        }

        private void On(object sender, RoutedEventArgs e)
        {
            MainWindow.plcConnect.writeBoolValue(MainWindow.plcConnect.getVariables().Find(p => p.Name.Equals(Name + "_OPEN_CLOSE")).Source, false);
            OnOff.Content = "ON";
        }

        private string checkMode(string mode)
        {
            if (mode.Equals("True"))
            {
                return "MANUAL";
            }
            else
            {
                return "AUTO";
            }
        }

        private void modeGraphic(string mode)
        {
            if (mode.Equals("True"))
            {
                brdStatus.Background = Brushes.Orange;
                brdStatusTxt.Content = "M";
            }
            else
            {
                brdStatus.Background = Brushes.DeepSkyBlue;
                brdStatusTxt.Content = "A";
            }
        }

        private void faultGrapic(string fault)
        {
            if (fault.Equals("True"))
            {
                rctFault.Fill = Brushes.Red;
            }
            else
            {
                rctFault.Fill = Brushes.White;
            }
        }

        private void blockadeGraphic(string blockade)
        {
            if (blockade.Equals("True"))
            {
                eliBlockade.Fill = Brushes.Red;
            }
            else
            {
                eliBlockade.Fill = Brushes.LightGray;
            }
        }
    }
}
