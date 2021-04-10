using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private static System.Timers.Timer _timer1;

        private StateControl stateControl = new StateControl();
        private string Name { get; set; }

        private List<S7.Net.Types.DataItem> dataItemList = new List<S7.Net.Types.DataItem>();
        public static List<WriteBoolData> writeDataList = new List<WriteBoolData>();
        public WriteBoolData writeData = new WriteBoolData();

        public ValveStation(string Name)
        {
            InitializeComponent();

            _timer1 = new System.Timers.Timer(500); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;

            stationName.Content = Name;
            this.Name = Name;

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // Read valve state
            var uriSource = new Uri(stateControl.setValveImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgUV.Source = new BitmapImage(uriSource); }));
            Dispatcher.Invoke(new Action(() => { txtStatus.Text = stateControl.setValveTxt(MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_STATE")).Value); }));
            
            // Read auto/manual mode
            Dispatcher.Invoke(new Action(() => { txtMode.Text = checkMode(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_AM")).Value); }));
            Dispatcher.Invoke(new Action(() => { modeGraphic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_AM")).Value); }));

            //Read fault signal
            Dispatcher.Invoke(new Action(() => { faultGraphic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_FAULT")).Value); }));

            // Read blockade signal
            Dispatcher.Invoke(new Action(() => { blockadeGraphic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_BLOCKADE")).Value); }));
            
            dataItemList.Clear();
            dataItemList.AddRange(writeData.createDataList(writeDataList));
            //MainWindow.plcConnect.writeArray(dataItemList.ToArray());

            foreach (var item in writeDataList)
            {
                item.Value = false;
            }
        }


        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Hide();
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

        private void faultGraphic(string fault)
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

        private void autoClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_AUTO");
        }

        private void manualClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_MANUAL");
        }

        private void openClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_OPEN");
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_CLOSE");
        }

        private void sendSignal(string operation)
        {

            MainScreen.writeBoolDataList.Find(p => p.Name.Equals(Name + operation)).Value = true;
        }
    }
}
