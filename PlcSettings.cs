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
        private DataCollection dataCollection = new DataCollection();


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
            catch (S7.Net.PlcException ex)
            {

                MessageBox.Show(ex.Message + ex.ErrorCode, "Information");
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

        // Read real value from input address
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

                MessageBox.Show(ex.Message, "readRealValue");
                return 0;
            }
        }

        // Read int value from input address
        public int readIntValue(string address)
        {
            try
            {
                if (plc != null)
                {
                    var item = (ushort)plc.Read(address);
                    int result = item.ConvertToShort();
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (S7.Net.PlcException ex)
            {

                MessageBox.Show(ex.Message, "readIntValue");
                return 0;
            }
        }

        // Read boolean value from input address
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

                MessageBox.Show(ex.Message, "readBoolValue");
                return "False";
            }
        }

        // Write boolean value to input address
        public void writeBoolValue(string address, object value)
        {
            try
            {
                if (plc != null)
                {
                    plc.Write(address, value);
                }
            }
            catch (S7.Net.PlcException ex)
            {

                MessageBox.Show(ex.Message, "writeBoolValue");
            }
        }

        // Write real value from input address
        public void writeRealValue(string address, object value)
        {
            try
            {
                if (plc != null)
                {
                    plc.Write(address, value);
                }

            }
            catch (S7.Net.PlcException ex)
            {

                MessageBox.Show(ex.Message, "writeRealValue");

            }
        }

        // Write int value from input address
        public void writeIntValue(string address, object value)
        {
            try
            {
                if (plc != null)
                {
                    plc.Write(address, value);
                }

            }
            catch (S7.Net.PlcException ex)
            {

                MessageBox.Show(ex.Message, "writeIntValue");

            }
        }




        // Cyclic data monitor. Reads all variables from file variables.xlsx and perform certain operations on them.
        public void dataMonitor(DateTime actualTime, IEnumerable<Variables> variables)
        {
            foreach (var variable in variables)
            {
                dataCollection.collectData(actualTime, variable);
                alarmHandling(actualTime, variable);
            }
        }

        // Cyclic send Bool values
        public void sendBoolData(List<WriteBoolData> dataBoolList)
        {
            foreach (var item in dataBoolList)
            {
                writeBoolValue(item.Address, item.Value);
            }
        }

        // Cycli send Real values
        public void sendRealData(List<WriteRealData> dataRealList)
        {
            foreach (var item in dataRealList)
            {
                writeRealValue(item.Address, item.Value);
            }
        }

        public void alarmHandling(DateTime actualTime, Variables variable)
        {

            if (variable.Alarm == true)
            {
                switch (variable.Type)
                {
                    case "BOOL":
                        if (alarmInList == null && readBoolValue(variable.Source).Equals("True"))
                        {
                            alarmInList.Add(new AlarmList() { TimeReceived = actualTime.ToShortDateString() + " " + actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });

                        }
                        else if (alarmInList != null)
                        {
                            if (alarmInList.Any(p => p.VariableName.Equals(variable.Name)) == false && readBoolValue(variable.Source).Equals("True"))
                            {
                                alarmInList.Add(new AlarmList() { TimeReceived = actualTime.ToShortDateString() + " " + actualTime.ToLongTimeString(), VariableName = variable.Name, AlarmValue = 1, Text = variable.AlarmText, Active = true });

                            }
                            else if (alarmInList.Any(p => p.VariableName.Equals(variable.Name)) == true && readBoolValue(variable.Source).Equals("False"))
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

            }
        }

        public IEnumerable<AlarmList> getAlarmList()
        {
            return alarmInList;
        }

        public void turnOffDataCollection()
        {
            dataCollection.turnOff();
        }

        public List<Data> getData()
        {
            return dataCollection.getData();
        }

        public List<State> getState()
        {
            return dataCollection.getState();
        }

        public List<Mode> getMode()
        {
            return dataCollection.getMode();
        }

        public List<Variables> getVariables()
        {
            return variables;
        }

        public void setVariables(List<Variables> variables)
        {
            this.variables = variables;
        }

        public void writeArray(S7.Net.Types.DataItem[] dataitems)
        {
            Task.Run(() =>
            {
                plc.Write(dataitems);
            });
        }


    }
}
