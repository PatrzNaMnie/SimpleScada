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

namespace SimpleScada.Screens.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private static Timer _timer1;

        
        private List<ValveStation> valveStations = new List<ValveStation>();
        private StateControl stateControl = new StateControl();
        public Home()
        {
            InitializeComponent();


            _timer1 = new Timer(250); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;
            


        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            

            Dispatcher.Invoke(new Action(() => { TankLvlBar.Value = Convert.ToDouble(MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals("LI_1")).Last().Value); }));

            var uriSource = new Uri(stateControl.setValveImg(MainWindow.plcConnect.getState().Find(p => p.Name.Equals("UV_1_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgUV1.Source = new BitmapImage(uriSource); }));
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
                valveStations.First().Show();
            }
        }
    }
}
