using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleScada
{
    class StateControl
    {
        public string setValveImg(int State)
        {
            switch (State)
            {
                case 0:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumZamknięty.png";

                case 1:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumOtwZam.png";

                case 2:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumOtwarty.png";
                case 3:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumOtwZam.png";

                case 4:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/ZawórPneumAwaria.png";

                default:
                    return "";

            }
        }

        public string setValveTxt(int State)
        {
            switch (State)
            {
                case 0:
                    return "CLOSED";

                case 1:
                    return "CLOSING";

                case 2:
                    return "OPEN";
                case 3:
                    return "OPENING";

                case 4:
                    return "FAULT";

                default:
                    return "";

            }
        }
    }
}
