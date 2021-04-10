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
        DateTime actualTime;
        public static SeriesCollection SeriesCollection { get; set; }
        public static List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private List<Data> data = new List<Data>();

        private string actualMeasuringPoint;

        private System.Timers.Timer _timer1;

        private int counter = 0;
        private int result = 0;
        public Trend()
        {

            InitializeComponent();

            actualTime = DateTime.Now;

            _timer1 = new System.Timers.Timer(1000); //Updates every second.
            _timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);


            SeriesCollection = new SeriesCollection { };
            Labels = new List<string> { };
            YFormatter = value => Math.Round(value, 2).ToString("") + MainScreen.variables.Find(p => p.Name.Equals(data.Last().MeasuringPoin)).MeasuringUnit;


            // Default measuring point will be LI_1

            actualMeasuringPoint = "LI_1";

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
                counter = 60;
                foreach (var item in data)
                {
                    result = DateTime.Compare(Convert.ToDateTime(item.Time).AddSeconds(60), Convert.ToDateTime(actualTime.ToLongTimeString()));
                    if (result < 0)
                    {
                        SeriesCollection[0].Values.Add(0.0);
                        Labels.Add(actualTime.AddSeconds(counter).ToLongTimeString());
                    }
                    else
                    {
                        SeriesCollection[0].Values.Add(Math.Round(Convert.ToDouble(item.Value), 2));
                        Labels.Add(item.Time);
                    }
                    counter--;

                }

                Labels.Reverse();
                _timer1.Enabled = true;
            });


            DataContext = this;
        }


        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            actualData(actualMeasuringPoint);

        }

        // Binding TrendMeasuringPoints to check box
        // Cases is the measuring Points
        private void cboxMeasuringPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch (cboxMeasuringPoint.SelectedItem.ToString())
            {
                case "LI_1":
                    createSeriesCollection(cboxMeasuringPoint.SelectedItem.ToString());
                    break;

                case "LI_2":
                    createSeriesCollection(cboxMeasuringPoint.SelectedItem.ToString());
                    break;

                case "FI_1":
                    createSeriesCollection(cboxMeasuringPoint.SelectedItem.ToString());
                    break;

                case "FI_2":
                    createSeriesCollection(cboxMeasuringPoint.SelectedItem.ToString());
                    break;

                case "AI_1":
                    createSeriesCollection(cboxMeasuringPoint.SelectedItem.ToString());
                    break;
            }

        }

        // Create a series collection of specific measuring point choosen in check box
        private void createSeriesCollection(string measuringPoint)
        {
            actualTime = DateTime.Now;
            _timer1.Enabled = false;
            SeriesCollection.Clear();
            Labels.Clear();
            actualMeasuringPoint = measuringPoint;
            SeriesCollection.Add(new LineSeries
            {
                Title = measuringPoint,
                Values = new ChartValues<double> { }
            });

            Task.Run(() =>
            {

            using (var db = new SimpleScadaContext())
            {

                data = db.Data.Where(p => p.MeasuringPoin.Equals(measuringPoint)).OrderByDescending(p => p.Id).Take(60).ToList();

            }
            counter = 60;

            YFormatter = null;
            YFormatter = value => value.ToString("") + MainScreen.variables.Find(p => p.Name.Equals(data.Last().MeasuringPoin)).MeasuringUnit;

                foreach (var item in data)
                {
                    result = DateTime.Compare(Convert.ToDateTime(item.Time), actualTime.AddSeconds(60));
                    if (result > 0)
                    {
                        SeriesCollection[0].Values.Add(0.0);
                        Labels.Add(actualTime.AddSeconds(counter).ToLongTimeString());
                    }
                    else
                    {
                        SeriesCollection[0].Values.Add(Convert.ToDouble(item.Value));
                        Labels.Add(item.Time);
                    }
                    counter--;

                }
                Labels.Reverse();
                _timer1.Enabled = true;
            });
        }

        // Get the last measured value of choosen measuring point and add it to series collection
        private void actualData(string measuringPoint) 
        {
            var dataCollected = MainWindow.plcConnect.getData().Where(p => p.MeasuringPoin.Equals(measuringPoint)).Last();
            SeriesCollection[0].Values.Remove(Convert.ToDouble(SeriesCollection[0].Values[0].ToString()));
            Labels.Remove(Labels[0].ToString());
            SeriesCollection[0].Values.Add(Math.Round(Convert.ToDouble(dataCollected.Value), 2));
            Labels.Add(dataCollected.Time);
        }
    }
}
