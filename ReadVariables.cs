using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleScada
{
    public class ReadVariables
    {
        private IEnumerable<Variables> variables;


        public IEnumerable<Variables> readVar()
        {
            var filename = AppDomain.CurrentDomain.BaseDirectory + "//Excel/Variables.xlsx";
            variables = new ExcelMapper(filename).Fetch<Variables>();
            return variables;
        }


    }
}
