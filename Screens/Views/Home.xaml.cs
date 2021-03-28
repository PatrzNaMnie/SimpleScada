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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleScada.Screens.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private static System.Timers.Timer _timer1;

        
        private List<ValveStation> valveStations = new List<ValveStation>();
        private List<PumpStation> pumpStations = new List<PumpStation>();
        private StateControl stateControl = new StateControl();
        private List<AutomaticControlStations> autoStations = new List<AutomaticControlStations>();
        public Home()
        {
            InitializeComponent();


            _timer1 = new System.Timers.Timer(500); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;
            


        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            
            // Tank T1 Level 
            Dispatcher.Invoke(new Action(() => { TankT1.Value = Convert.ToDouble(MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals("LI_1")).Last().Value); }));

            // Tank T2 Level 
            Dispatcher.Invoke(new Action(() => { TankT1.Value = Convert.ToDouble(MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals("LI_1")).Last().Value); }));

            // Flow FI 1
            Dispatcher.Invoke(new Action(() => { FI_1_Value.Text = Convert.ToDouble(MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals("FI_1")).Last().Value).ToString() + MainWindow.plcConnect.getVariables().Where(p => p.Name.Equals("FI_1")).Last().MeasuringUnit; }));

            // Flow FI 1
            Dispatcher.Invoke(new Action(() => { FI_1_Value.Text = Convert.ToDouble(MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals("FI_2")).Last().Value).ToString() + MainWindow.plcConnect.getVariables().Where(p => p.Name.Equals("FI_2")).Last().MeasuringUnit; }));

            // Ph AI 1
            Dispatcher.Invoke(new Action(() => { AI_1_Value.Text = Convert.ToDouble(MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals("AI_1")).Last().Value).ToString() + MainWindow.plcConnect.getVariables().Where(p => p.Name.Equals("AI_1")).Last().MeasuringUnit; }));

            // Graphic visualisation of Valve UV1 Status
            var uriUV1Source = new Uri(stateControl.setValveImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals("UV_1_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgUV1.Source = new BitmapImage(uriUV1Source); }));

            // Graphic visualisation of Valve UV1 Status
            var uriUV2Source = new Uri(stateControl.setValveImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals("UV_2_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgUV2.Source = new BitmapImage(uriUV2Source); }));

            // Graphic visualisation of Valve P1 Status
            var uriP1Source = new Uri(stateControl.setPumpImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals("P1_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgP1.Source = new BitmapImage(uriP1Source); }));

            // Graphic visualisation of Valve P2 Status
            var uriP2Source = new Uri(stateControl.setPumpImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals("P2_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgP2.Source = new BitmapImage(uriP2Source); }));

            // Graphic visualisation of Valve P3 Status
            var uriP3Source = new Uri(stateControl.setPumpImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals("P3_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgP3.Source = new BitmapImage(uriP3Source); }));


            // Graphic visualisation of Very High Level on T1
            var uriT1_HHSource = new Uri(stateControl.setSignalLampImg(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals("LSHH_T1")).Value));
            Dispatcher.Invoke(new Action(() => { SignalLampT1_HH.Source = new BitmapImage(uriT1_HHSource); }));

            // Graphic visualisation of Very High Level on T1
            var uriT1_LLSource = new Uri(stateControl.setSignalLampImg(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals("LSHH_T1")).Value));
            Dispatcher.Invoke(new Action(() => { SignalLampT1_LL.Source = new BitmapImage(uriT1_LLSource); }));

            // Graphic visualisation of Very High Level on T2
            var uriT2_HHSource = new Uri(stateControl.setSignalLampImg(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals("LSHH_T2")).Value));
            Dispatcher.Invoke(new Action(() => { SignalLampT2_HH.Source = new BitmapImage(uriT2_HHSource); }));

            // Graphic visualisation of Very High Level on T2
            var uriT2_LLSource = new Uri(stateControl.setSignalLampImg(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals("LSHH_T2")).Value));
            Dispatcher.Invoke(new Action(() => { SignalLampT2_LL.Source = new BitmapImage(uriT2_LLSource); }));

            // Graphic visualisation of Very High Level on CT
            var uriCT_HSource = new Uri(stateControl.setSignalLampImg(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals("LSH_CT")).Value));
            Dispatcher.Invoke(new Action(() => { SignalLampCT_H.Source = new BitmapImage(uriCT_HSource); }));

            // Graphic visualisation of Very High Level on CT
            var uriCT_LSource = new Uri(stateControl.setSignalLampImg(MainWindow.plcConnect.getMode().Find(p => p.Name.Equals("LSH_CT")).Value));
            Dispatcher.Invoke(new Action(() => { SignalLampCT_L.Source = new BitmapImage(uriCT_LSource); }));




        }

        public static void stopTimer()
        {
            _timer1.Enabled = false;
        }


        private void UV1_Open_Station(object sender, RoutedEventArgs e)
        {
            if (valveStations.Exists(p => p.Name.Equals("UV_1")))
            {
                valveStations.Find(p => p.Name.Equals("UV_1")).Show();
            }
            else
            {

                valveStations.Add(new ValveStation(Name = "UV_1"));
                valveStations.Last().Show();
            }
        }


        private void onClick(object sender, RoutedEventArgs e)
        {


        }

        private void P1_Open_Station(object sender, RoutedEventArgs e)
        {
            if (pumpStations.Exists(p => p.Name.Equals("P1")))
            {
                pumpStations.Find(p => p.Name.Equals("P1")).Show();
            }
            else
            {
                pumpStations.Add(new PumpStation(Name = "P1"));
                pumpStations.Last().Show();
            }
        }

        private void UV2_Open_Station(object sender, RoutedEventArgs e)
        {
            if (valveStations.Exists(p => p.Name.Equals("UV_2")))
            {
                valveStations.Find(p => p.Name.Equals("UV_2")).Show();
            }
            else
            {

                valveStations.Add(new ValveStation(Name = "UV_2"));
                valveStations.Last().Show();
            }
        }

        private void P2_Open_Station(object sender, RoutedEventArgs e)
        {
            if (pumpStations.Exists(p => p.Name.Equals("P2")))
            {
                pumpStations.Find(p => p.Name.Equals("P2")).Show();
            }
            else
            {
                pumpStations.Add(new PumpStation(Name = "P2"));
                pumpStations.Last().Show();
            }
        }

        private void P3_Open_Station(object sender, RoutedEventArgs e)
        {
            if (pumpStations.Exists(p => p.Name.Equals("P3")))
            {
                pumpStations.Find(p => p.Name.Equals("P3")).Show();
            }
            else
            {
                pumpStations.Add(new PumpStation(Name = "P3"));
                pumpStations.Last().Show();
            }
        }

        private void FillingT1StationOpen(object sender, RoutedEventArgs e)
        {
            if (autoStations.Exists(p => p.Name.Equals("FILL_T1")))
            {
                autoStations.Find(p => p.Name.Equals("FILL_T1")).Show();
            }
            else
            {

                autoStations.Add(new AutomaticControlStations(Name = "FILL_T1"));
                autoStations.Last().Show();
            }
        }

        private void TransferToT2StationOpen(object sender, RoutedEventArgs e)
        {
            if (autoStations.Exists(p => p.Name.Equals("TRANSFER_TO_T2")))
            {
                autoStations.Find(p => p.Name.Equals("TRANSFER_TO_T2")).Show();
            }
            else
            {

                autoStations.Add(new AutomaticControlStations("TRANSFER_TO_T2", "LI_2_SP", "P1_SP"));
                autoStations.Last().Show();
            }
        }

        private void DosingChemicalsStationOpen(object sender, RoutedEventArgs e)
        {
            if (autoStations.Exists(p => p.Name.Equals("DOSE_CHEMICALS")))
            {
                autoStations.Find(p => p.Name.Equals("DOSE_CHEMICALS")).Show();
            }
            else
            {

                autoStations.Add(new AutomaticControlStations("DOSE_CHEMICALS"));
                autoStations.Last().Show();
            }
        }

        private void EmptyiongT2StationOpen(object sender, RoutedEventArgs e)
        {
            if (autoStations.Exists(p => p.Name.Equals("EMPTYING_T2")))
            {
                autoStations.Find(p => p.Name.Equals("EMPTYING_T2")).Show();
            }
            else
            {

                autoStations.Add(new AutomaticControlStations("EMPTYING_T2", "P2_SP"));
                autoStations.Last().Show();
            }
        }
    }
}
