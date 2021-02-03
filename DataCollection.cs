using SimpleScada.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleScada
{
    public class DataCollection
    {
        DateTime actualTime;

        private List<Data> data = new List<Data>();


        /*public DataCollection(List<Variables> variables)
        {
            stopCollectData = true;
            
            Task.Run(() =>
            {
                while (stopCollectData)
                {
                    Thread.Sleep(1000);
                    actualTime = DateTime.Now;
                    foreach (var variable in variables)
                    {
                        if(variable.Type.Equals("REAL"))
                        data.Add(new Data() { Date = actualTime.ToShortDateString(), Time = actualTime.ToLongTimeString(), MeasuringPoin=variable.Name, Value=MainWindow.plcConnect.readRealValue(variable.Source).ToString() });

                    }

                   
                }

            });
        }*/

        public void collectData(DateTime actualTime, Variables variable)
        {
            try
            {
                if (variable.Historian.Equals("True"))
                {
                    data.Add(new Data() { Date = actualTime.ToShortDateString(), Time = actualTime.ToLongTimeString(), MeasuringPoin = variable.Name, Value = MainWindow.plcConnect.readRealValue(variable.Source).ToString() });
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Warning");
            }

            
        }

        public void turnOff()
        {
            using (var db = new SimpleScadaContext())
            {

                    db.Data.AddRange(data);
                    db.SaveChanges();
            }
            
        }

        public List<Data> getData()
        {
            return data;
        }

    }
}
