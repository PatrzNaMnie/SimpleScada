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
        private IEnumerable<Variables> people;

        public IEnumerable<Variables> readVar()
        {
            var filename = AppDomain.CurrentDomain.BaseDirectory + "//Excel/Variables.xlsx";
            people = new ExcelMapper(filename).Fetch<Variables>();
            return people;
        }


    }
}
