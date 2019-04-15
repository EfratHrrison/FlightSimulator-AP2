using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class JoystickVM 
    {
        //string[] input;
        string line;
       
        public float rudderCommand
        {
            set
            {
                string rudder = "set controls/flight/rudder ";
                rudder += " ";
                rudder += value;
                rudder += " ";
                rudder += "/r/n";
                //CommandS command = new CommandS();
                CommandS.Instance.openThread(rudder);
            }
        }
   

        public float throttleCommand
        {
            set
            {
                string throttle = "controls/engines/current-engine/throttle ";
                throttle += " ";
                throttle += line;
                throttle += " ";
                throttle += "/r/n";
                //CommandS command = new CommandS();
                CommandS.Instance.openThread(throttle);
            }
        }
    }
}

