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
    /// Interaction logic for HistoryAlarm.xaml
    /// </summary>
    public partial class HistoryAlarm : UserControl
    {
        private Timer _timer1;
        private List<AlarmHistory> alarmHistory = new List<AlarmHistory>();
        public HistoryAlarm()
        {
            InitializeComponent();

            _timer1 = new Timer(1000); //Updates every half second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer1.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            // Top bar info labels
            using (var db = new SimpleScadaContext())
            {
                alarmHistory.Clear();
                alarmHistory.AddRange((IEnumerable<AlarmHistory>)db.AlarmHistory.ToList());
            }

            Dispatcher.Invoke(new Action(() => { alarmHistoryList.ItemsSource = null; }));
            Dispatcher.Invoke(new Action(() => { alarmHistoryList.ItemsSource = alarmHistory; }));

        }
    }
}
