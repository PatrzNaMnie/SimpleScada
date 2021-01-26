using System;
using S7.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleScada
{
    public class PlcSettings
    {
        private Plc plc = null;
        private List<AlarmList> alarmInList = new List<AlarmList>();
        private List<Variables> variables = new List<Variables>();


        public enum dataTypes
        {
            BOOl,
            BYTE,
            INT,
            DINT,
            WORD,
            DWORD,
            REAL,
            TIME

        }

        public void Connect(string CpuType, string IpAddress, string Rack, String Slot)
        {
            try
            {
                CpuType cpuType = (CpuType)Enum.Parse(typeof(CpuType), (string)(CpuType));
                string ipAddress = IpAddress;
                short rack = short.Parse(Rack);
                short slot = short.Parse(Slot);
                plc = new Plc(cpuType, ipAddress, rack, slot);
                plc.Open();
                //btnConnect.Enabled = false;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Information");
            }
        }

        public void Disconnect()
        {
            plc.Close();
        }

        public bool IsConnected()
        {
            if (plc.IsConnected)
            {
                return true;
            }
            else
                return false;
        }

        public double readRealValue(string address)
        {
            try
            {
                if (plc != null)
                {
                    var item = (uint)plc.Read(address);
                    double result = item.ConvertToFloat();
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (S7.Net.PlcException ex)
            {

                MessageBox.Show(ex.Message, "Information");
                return 0;
            }
        }

        public string readBoolValue(string address)
        {
            try
            {
                if (plc != null)
                {
                    object item = plc.Read(address);
                    return item.ToString();
                }
                else
                {
                    return "False";
                }
            }
            catch (S7.Net.PlcException ex)
            {

                MessageBox.Show(ex.Message, "Information");
                return "False";
            }
        }

        public IEnumerable<AlarmList> alarmHandling(DateTime actualTime , IEnumerable<Variables> variables)
        {
            foreach (var variable in variables)
            {
                if (variable.Alarm == true)
                {
                    switch (variable.Type)
                    {
                        case "BOOL":
                            if (alarmInList == null && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("True"))
                            {
                                alarmInList.Add(new AlarmList() { TimeReceived = actualTime.ToShortDateString()+ " " +actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });

                            }
                            else if (alarmInList != null)
                            {
                                if (alarmInList.Any(p => p.VariableName.Equals(variable.Name)) == false && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("True"))
                                {
                                    alarmInList.Add(new AlarmList() { TimeReceived = actualTime.ToShortDateString() + " " + actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });

                                }
                                else if (alarmInList.Any(p => p.VariableName.Equals(variable.Name)) == true && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("False"))
                                {

                                    var tempAlarm = alarmInList.First(p => p.VariableName.Equals(variable.Name)) as AlarmList;

                                    using (var db = new SimpleScadaContext())
                                    {
                                        db.AlarmHistory.Add(new AlarmHistory()
                                        {
                                            TimeReceived = tempAlarm.TimeReceived,
                                            TimeAcknowledge = actualTime.ToShortDateString() + " " + actualTime.ToLongTimeString(),
                                            VariableName = tempAlarm.VariableName,
                                            Text = tempAlarm.Text,
                                            AlarmValue = tempAlarm.AlarmValue
                                        });
                                        db.SaveChanges();
                                    }

                                    alarmInList.Remove(tempAlarm);
                                }
                            }
                            break;
                        case "REAL":
                            if (alarmInList == null && Convert.ToDouble(MainWindow.plcConnect.readBoolValue(variable.Source)) > variable.AlarmLimitMin)
                            {
                                alarmInList.Add(new AlarmList() { TimeReceived = actualTime.ToShortDateString() + " " + actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });

                            }
                            else if (alarmInList != null)
                            {
                                if (alarmInList.Any(p => p.VariableName.Equals(variable.Name)) == false && Convert.ToDouble(MainWindow.plcConnect.readBoolValue(variable.Source)) > variable.AlarmLimitMin)
                                {
                                    alarmInList.Add(new AlarmList() { TimeReceived = actualTime.ToShortDateString() + " " + actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });

                                }
                                else if (alarmInList.Any(p => p.VariableName.Equals(variable.Name)) == true && Convert.ToDouble(MainWindow.plcConnect.readBoolValue(variable.Source)) < variable.AlarmLimitMin)
                                {

                                    var tempAlarm = alarmInList.First(p => p.VariableName.Equals(variable.Name)) as AlarmList;

                                    using (var db = new SimpleScadaContext())
                                    {
                                        db.AlarmHistory.Add(new AlarmHistory()
                                        {
                                            TimeReceived = tempAlarm.TimeReceived,
                                            TimeAcknowledge = actualTime.ToShortDateString() + " " + actualTime.ToLongTimeString(),
                                            VariableName = tempAlarm.VariableName,
                                            Text = tempAlarm.Text,
                                            AlarmValue = tempAlarm.AlarmValue
                                        });
                                        db.SaveChanges();
                                    }

                                    alarmInList.Remove(tempAlarm);
                                }
                            }
                            break;

                        default:
                            break;
                    }

                    /*if (alarmInList == null && variable.Type.Equals("BOOL") && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("True"))
                    {
                        alarmInList.Add(new AlarmList() { TimeReceived = actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });

                    }
                    else if (alarmInList != null)
                    {
                        if (alarmInList.Any(p => p.VariableName.Equals(variable.Name)) == false && variable.Type.Equals("BOOL") && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("True"))
                        {
                            alarmInList.Add(new AlarmList() { TimeReceived = actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });

                        }
                        else if (alarmInList.Any(p => p.VariableName.Equals(variable.Name)) == true && MainWindow.plcConnect.readBoolValue(variable.Source).Equals("False"))
                        {

                            var tempAlarm = alarmInList.First(p => p.VariableName.Equals(variable.Name)) as AlarmList;

                            using (var db = new SimpleScadaContext())
                            {
                                db.AlarmHistory.Add(new AlarmHistory()
                                {
                                    TimeReceived = tempAlarm.TimeReceived,
                                    TimeAcknowledge = actualTime.ToLongTimeString(),
                                    VariableName = tempAlarm.VariableName,
                                    Text = tempAlarm.Text,
                                    AlarmValue = tempAlarm.AlarmValue
                                });
                                db.SaveChanges();
                            }

                            alarmInList.Remove(tempAlarm);
                        }
                    }*/

                }
            }
            return alarmInList;
        }

    }
}
