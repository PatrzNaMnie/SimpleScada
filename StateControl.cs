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

        public string setPumpImg(int State)
        {
            switch (State)
            {
                case 0:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/PompaStop.png";

                case 1:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/PompaStart.png";

                case 2:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/PompaStart.png";

                case 3:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/PompaStop.png";

                case 4:
                    return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/PompaAwaria.png";

                default:
                    return "";

            }
        }

        public string setPumpTxt(int State)
        {
            switch (State)
            {
                case 0:
                    return "STOP";

                case 1:
                    return "STARTING UP";

                case 2:
                    return "RUNNING";

                case 3:
                    return "STOPPING";

                case 4:
                    return "FAULT";

                default:
                    return "";

            }
        }

        public string setSignalLampImg(string state)
        {
            if (state.Equals("True"))
            {
                return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/LampkaPhOff.png";
            }
            else
                return "C:/Programowanie/WPF/ProjektScadaMes/SimpleScada/SimpleScada/Images/LampkaPhOn.png";
        }
    }
}
