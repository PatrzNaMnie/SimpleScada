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

        
        private List<ValveStation> valveStations;
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

            var uriSource = new Uri(setValveState(MainWindow.plcConnect.GetState().Find(p => p.Name.Equals("UV_1_STATE")).Value));
            Dispatcher.Invoke(new Action(() => { ImgUV1.Source = new BitmapImage(uriSource); }));
        }

        public static void stopTimer()
        {
            _timer1.Enabled = false;
        }

        private string setValveState(int State)
        {
            switch (State)
            {
                case 0:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumZamknięty.png";

                case 1: 
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumOtwZam.png";

                case 2:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumOtwarty.png";
                case 3:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumOtwZam.png";

                case 4:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumAwaria.png";

                default:
                    return "";

            }
        }

        private void UV1_Open_Station(object sender, RoutedEventArgs e)
        {
            if (valveStations != null)
            {
                valveStations.Clear();
            }
            valveStations.Add(new ValveStation(Name = "UV_1"));
            valveStations.First().Show();
        }
    }
}
