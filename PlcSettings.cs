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

        public string readBoolValue(string address)
        {
            object item = plc.Read(address);
            return item.ToString();
        }

    }
}
