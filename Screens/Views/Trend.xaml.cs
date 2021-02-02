using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        private int counter;

        static SimpleScada.Trend trend1;
        static SimpleScada.Trend trend2;

        public Trend()
        {

            InitializeComponent();

            trend1 = new SimpleScada.Trend("Series 1", 60, 1000);
            trend2 = new SimpleScada.Trend("Series 2", 60, 1000);

            SeriesCollection = new SeriesCollection
            {
                new LineSeries{ },
                new LineSeries{ }
               /* new LineSeries
                {
                    Title = "Series 1",
                    //Values = new ChartValues<double> { 0, 0, 0, 0, 0 }
                    Values = new ChartValues<double> {}
                },*/

            };

            /*SeriesCollection.Add(new LineSeries
            {
                Title = "Series 2",
                Values = new ChartValues<double> { 0, 0, 0, 0, 0 },
                LineSmoothness = 1, //0: straight lines, 1: really smooth lines
            });*/



            //Labels = new List<string> { actualTime.AddSeconds(-40).ToString(), actualTime.AddSeconds(-30).ToString(),
            //actualTime.AddSeconds(-20).ToString(), actualTime.AddSeconds(-10).ToString(), actualTime.ToString()};
            Labels = new List<string>{};
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart


            /*Task.Run(() =>
            {
                var r = new Random();
                while (true)
                {
                    counter = counter + 1;
                    Thread.Sleep(1000);
                    actualTime = DateTime.Now;
                    _trend += (r.NextDouble() > 0.3 ? 1 : -1) * r.Next(0, 5);
                    if (counter > 10)
                    {
                        Labels.Remove(Labels[0].ToString());
                        Labels.Add(actualTime.ToString());
                        SeriesCollection[0].Values.Remove(Convert.ToDouble(SeriesCollection[0].Values[0].ToString()));
                        SeriesCollection[0].Values.Add(_trend);
                    }
                    else
                    {
                        Labels.Add(actualTime.ToString());
                        SeriesCollection[0].Values.Add(_trend);
                    }
                }
            });*/

            /*Task.Run(() =>
            {
                drawChart(trend1, 0);
                drawChart(trend2, 1);
            });*/

            DataContext = this;
        }

        public void drawChart(SimpleScada.Trend trend, int numOfLineSeries)
        {
            Dispatcher.Invoke(new Action(() => { SeriesCollection[numOfLineSeries] = trend.SeriesCollection[0]; ; }));
            Dispatcher.Invoke(new Action(() => { Labels = trend.Labels; ; }));

        }


    }
}
