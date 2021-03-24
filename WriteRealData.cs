using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleScada
{
    public class WriteRealData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Value { get; set; }


        public List<WriteRealData> createWriteDataList(List<Variables> variables)
        {
            List<WriteRealData> outputList = new List<WriteRealData>();
            foreach (var item in variables)
            {
                if (item.Alarm.Equals("False"))
                    outputList.Add(new WriteRealData() { Name = item.Name, Address = item.Source, Value = 0 });
            }
            return outputList;
        }

        public List<S7.Net.Types.DataItem> createDataList(List<WriteRealData> writeData)
        {
            List<S7.Net.Types.DataItem> outputList = new List<S7.Net.Types.DataItem>();

            foreach (var item in writeData)
            {
                outputList.Add(S7.Net.Types.DataItem.FromAddressAndValue(item.Address, item.Value));

            }
            return outputList;
        }
    }
}
