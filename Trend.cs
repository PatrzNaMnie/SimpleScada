using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using SimpleScada.Screens.Views;
using System.Windows.Threading;

namespace SimpleScada
{
    public class Trend
    {
        private double _trend;
        private int counter;
        DateTime actualTime;
        private int timeStamp;
        private int sampleTime;
        public  SeriesCollection SeriesCollection { get; private set; }

        public  List<string> Labels { get; set; }
        public Trend(string Title, int timeStamp, int sampleTime)
        {
            SeriesCollection = new SeriesCollection
            {
                
                new LineSeries{
                Title = Title,
                Values = new ChartValues<double> { },
                PointGeometry = null

                }

            };

            Labels = new List<string> { };

            this.timeStamp = timeStamp;
            Task.Run(() =>
            {
                var r = new Random();
                while (true)
                {
                    counter = counter + 1;
                    Thread.Sleep(sampleTime);
                    actualTime = DateTime.Now;
                    _trend += (r.NextDouble() > 0.3 ? 1 : -1) * r.Next(0, 5);
                    if (counter > timeStamp)
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
            });
        }

    }
}
