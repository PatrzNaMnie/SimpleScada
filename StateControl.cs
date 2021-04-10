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
                    return "pack://application:,,,/SimpleScada;component/Images/ZawórPneumZamknięty.png";
                
                case 1:
                    return "pack://application:,,,/SimpleScada;component/Images/ZawórPneumOtwZam.png";

                case 2:
                    return "pack://application:,,,/SimpleScada;component/Images/ZawórPneumOtwarty.png";
                case 3:
                    return "pack://application:,,,/SimpleScada;component/Images/ZawórPneumOtwZam.png";

                case 4:
                    return "pack://application:,,,/SimpleScada;component/Images/ZawórPneumAwaria.png";

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
                    return "pack://application:,,,/SimpleScada;component/Images/PompaStop.png"; 

                case 1:
                    return "pack://application:,,,/SimpleScada;component/Images/PompaStart.png";

                case 2:
                    return "pack://application:,,,/SimpleScada;component/Images/PompaStart.png";

                case 3:
                    return "pack://application:,,,/SimpleScada;component/Images/PompaStop.png";

                case 4:
                    return "pack://application:,,,/SimpleScada;component/Images/PompaAwaria.png";

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
                return "pack://application:,,,/SimpleScada;component/Images/LampkaPhOff.png";
                
            }
            else
                return "pack://application:,,,/SimpleScada;component/Images/LampkaPhOn.png";
        }

        public string fillingT1Txt(int State)
        {
            switch (State)
            {
                case 0:
                    return "STOP";

                case 1:
                    return "FILLING ";

                case 2:
                    return "NO CONDITIONS TO RUN";

                default:
                    return "";

            }
        }

        public string transferToT2Txt(int State)
        {
            switch (State)
            {
                case 0:
                    return "STOP";

                case 1:
                    return "TRANSFER TO T2";

                case 2:
                    return "ADDING CHEMICALS";

                case 3:
                    return "NO CONDITIONS TO RUN";


                default:
                    return "";

            }
        }

        public string dosingChemicalsTxt(int State)
        {
            switch (State)
            {
                case 0:
                    return "STOP";

                case 1:
                    return "ADDING CHEMICALS";

                case 2:
                    return "NO CONDITIONS TO RUN";


                default:
                    return "";

            }
        }

        public string emptyingT2Txt(int State)
        {
            switch (State)
            {
                case 0:
                    return "STOP";

                case 1:
                    return "EMPTYING T2";

                case 2:
                    return "NO CONDITIONS TO RUN";


                default:
                    return "";

            }
        }

        public string chooseProgramState(string name, int State)
        {
            switch (name)
            {
                case "FILL_T1":
                    return fillingT1Txt(State);

                case "TRANSFER_TO_T2":
                    return transferToT2Txt(State);

                case "DOSE_CHEMICALS":
                    return dosingChemicalsTxt(State);

                case "EMPTYING_T2":
                    return emptyingT2Txt(State);

                default:
                    return "";

            }
        }

    }
}
