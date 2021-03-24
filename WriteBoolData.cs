using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleScada
{
    public class WriteBoolData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Value { get; set; }


        public List<WriteBoolData> createWriteDataList (List<Variables> variables)
        {
            List<WriteBoolData> outputList = new List<WriteBoolData>();
            foreach (var item in variables)
            {
                if(item.Alarm.Equals("False"))
                outputList.Add(new WriteBoolData(){ Name = item.Name, Address = item.Source, Value = false });
            }
            return outputList;
        }

        public List<S7.Net.Types.DataItem> createDataList(List<WriteBoolData> writeData)
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

