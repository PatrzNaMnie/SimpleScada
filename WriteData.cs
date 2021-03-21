using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleScada
{
    public class WriteData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Value { get; set; }


        public List<WriteData> createWriteDataList (List<Variables> variables)
        {
            List<WriteData> outputList = new List<WriteData>();
            foreach (var item in variables)
            {
                if(item.Alarm.Equals("False"))
                outputList.Add(new WriteData(){ Name = item.Name, Address = item.Source, Value = false });
            }
            return outputList;
        }

        public List<S7.Net.Types.DataItem> createDataList(List<WriteData> writeData)
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

