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
    public partial class PumpStation : Window
    {
        private static System.Timers.Timer _timer1;

        private StateControl stateControl = new StateControl();
        private string Name { get; set; }

        private List<S7.Net.Types.DataItem> dataItemList = new List<S7.Net.Types.DataItem>();
        public static List<WriteData> writeDataList = new List<WriteData>();
        public WriteData writeData = new WriteData();

        public PumpStation(string Name)
        {
            InitializeComponent();

            _timer1 = new System.Timers.Timer(500); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;

            stationName.Content = Name;
            this.Name = Name;

            /*foreach (var item in MainScreen.variables)
            {
                if (item.MeasuringUnit.Equals("Send") && item.Type.Equals("BOOL"))
                    writeDataList.Add(new WriteData() { Name = item.Name, Address = item.Source, Value = false });
            }*/
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // Read pump state
            var uriSource = new Uri(stateControl.setPumpImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgUV.Source = new BitmapImage(uriSource); }));
            Dispatcher.Invoke(new Action(() => { txtStatus.Text = stateControl.setPumpTxt(MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_STATE")).Value); }));

            // Read auto/manual mode
            Dispatcher.Invoke(new Action(() => { txtMode.Text = checkMode(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_MODE")).Value); }));
            Dispatcher.Invoke(new Action(() => { modeGraphic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_MODE")).Value); }));

            // Read fault signal
            Dispatcher.Invoke(new Action(() => { faultGraphic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_FAULT")).Value); }));

            //Read blockade signal
            Dispatcher.Invoke(new Action(() => { blockadeGraphic(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals(Name + "_BLOCKADE")).Value); }));

            // Read running time
            Dispatcher.Invoke(new Action(() => { txtHours.Text = MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_RUN_H")).Value.ToString(); }));
            Dispatcher.Invoke(new Action(() => { txtMinutes.Text = MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_RUN_M")).Value.ToString(); }));
            Dispatcher.Invoke(new Action(() => { txtSeconds.Text = MainWindow.plcConnect.getState().Find(p => p.Name.Equals(Name + "_RUN_S")).Value.ToString(); }));

            // Read PV value
            Dispatcher.Invoke(new Action(() => { txtPV.Text = Convert.ToDouble(MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals(Name + "_PV")).Last().Value).ToString(); }));

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

        private void startClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_START");
        }

        private void stopClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_STOP");
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            sendSignal("_RESET");
        }

        private void sendSignal(string operation)
        {
            /*Task.Run(() =>
            {
                MainWindow.plcConnect.writeBoolValue(MainWindow.plcConnect.getVariables().Find(p => p.Name.Equals(Name + operation)).Source, true);
            });*/
            MainScreen.writeDataList.Find(p => p.Name.Equals(Name + operation)).Value = true;
        }

        private void sendSP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Task.Run(() =>
                {
                    double value = new double();
                    Dispatcher.Invoke(new Action(() => { value = double.Parse(txtSP.Text, System.Globalization.CultureInfo.InvariantCulture); }));
                    Dispatcher.Invoke(new Action(() => { MainWindow.plcConnect.writeRealValue(MainWindow.plcConnect.getVariables().Find(p => p.Name.Equals(Name + "_SP")).Source, value); }));
                });
            }
        }
    }
}
