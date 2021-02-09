using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;


namespace SimpleScada.Screens.Views
{
    /// <summary>
    /// Interaction logic for Trend.xaml
    /// </summary>
    public partial class Trend : UserControl
    {
        private double _trend;
        DateTime actualTime = DateTime.Now;
        public static SeriesCollection SeriesCollection { get; set; }
        public static List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private List<Data> data = new List<Data>();

        private int counter;



        private System.Timers.Timer _timer1;

        public Trend()
        {

            InitializeComponent();


            _timer1 = new System.Timers.Timer(1000); //Updates every half second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);




            SeriesCollection = new SeriesCollection
            {
               /* new LineSeries
                {
                    Title = "Series 1",
                    //Values = new ChartValues<double> { 0, 0, 0, 0, 0 }
                    Values = new ChartValues<double> {}
                },*/

            };


            Labels = new List<string>{};
            YFormatter = value => value.ToString("C");

            SeriesCollection.Add(new LineSeries
            {
                Title = "LI_1",
                Values = new ChartValues<double> { }
            });

            Task.Run(() =>
            {

                using (var db = new SimpleScadaContext())
                {

                    data = db.Data.Where(p => p.MeasuringPoin.Equals("LI_1")).OrderByDescending(p => p.Id).Take(60).ToList();

                }
                foreach (var item in data)
                {

                    SeriesCollection[0].Values.Add(Convert.ToDouble(item.Value));
                    Labels.Add(item.Time);

                }
                Labels.Reverse();
                _timer1.Enabled = true;
            });






            DataContext = this;
        }

        public void drawChart(SimpleScada.Trend trend, int numOfLineSeries)
        {
            Dispatcher.Invoke(new Action(() => { SeriesCollection[numOfLineSeries] = trend.SeriesCollection[0]; ; }));
            Dispatcher.Invoke(new Action(() => { Labels = trend.Labels; ; }));

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            var dataCollected = MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals("LI_1")).Last();
            SeriesCollection[0].Values.Remove(Convert.ToDouble(SeriesCollection[0].Values[0].ToString()));
            Labels.Remove(Labels[0].ToString());
            SeriesCollection[0].Values.Add(Convert.ToDouble(dataCollected.Value));
            Labels.Add(dataCollected.Time);


        }

        private void LI_1_Click(object sender, RoutedEventArgs e)
        {

            SeriesCollection.Add(new LineSeries
            {
                Title = "LI_1",
                Values = new ChartValues<double> { }
            });


            Task.Run(() =>
            {

                using (var db = new SimpleScadaContext())
                {
                    data = db.Data.Where(p => p.MeasuringPoin.Equals("LI_1")).OrderByDescending(p => p.Id).Take(60).ToList();
                }

            });

            foreach (var item in data)
            {

                SeriesCollection[0].Values.Add(Convert.ToDouble(item.Value));
                Labels.Add(item.Time);

            }
        }
    }
}
