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

        private List<State> states = new List<State>();
        public DataCollection()
        {
            using (var db = new SimpleScadaContext())
            {
                var test = db.AlarmHistory.FirstOrDefault();
            }
        }

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

                // Collect states of valves and pumps from PLC and add it to list
                if (variable.Type.Equals("INT") && !states.Any(p => p.Name.Equals(variable.Name)))
                {
                    states.Add(new State() { Name = variable.Name, Value = MainWindow.plcConnect.readIntValue(variable.Source) });
                }
                else if (variable.Type.Equals("INT") && states.Any(p => p.Name.Equals(variable.Name)))
                {
                    states.Find(p => p.Name.Equals(variable.Name)).Value = MainWindow.plcConnect.readIntValue(variable.Source);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "collectData");
            }

            
        }

        public void turnOff()
        {
            Task.Run(() =>
            {
                
                using (var db = new SimpleScadaContext())
                {
                    db.Data.AddRange(data);
                    db.SaveChanges();
                }
            });
            
        }

        public List<Data> getData()
        {
            return data;
        }

        public List<State> getState()
        {
            return states;
        }



    }
}
