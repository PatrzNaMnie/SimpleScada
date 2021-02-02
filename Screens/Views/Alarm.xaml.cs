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
    /// Interaction logic for Alarm.xaml
    /// </summary>
    public partial class Alarm : UserControl
    {
        private Timer _timer1;
        public Alarm()
        {
            InitializeComponent();

            _timer1 = new Timer(1000); //Updates every half second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            // Top bar info labels

            Dispatcher.Invoke(new Action(() => { alarmList.ItemsSource = null; }));
            Dispatcher.Invoke(new Action(() => { alarmList.ItemsSource = MainWindow.plcConnect.getAlarmList(); }));
        }
    }
}
