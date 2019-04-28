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
     
       
        public float rudderCommand
        {
            set
            {
                //send the rudder updated value to simulator 
                string rudder = "set controls/flight/rudder ";
                rudder += " ";
                rudder += value;
                rudder += " ";
                rudder += "/r/n";
                CommandS.Instance.openThread(rudder);
            }
        }
   

        public float throttleCommand
        {
            set
            {
                //send the throttle updated value to simulator from the joystoick
                string throttle = "set controls/engines/current-engine/throttle ";
                throttle += " ";
                throttle += value;
                throttle += " ";
                throttle += "/r/n";
                CommandS.Instance.openThread(throttle);
            }
        }
        public float aileronCommand
        {
            set
            {
                //send the aileron updated value to simulator
                string aileron = "set /controls/flight/aileron ";
                aileron += " ";
                aileron += value;
                aileron += " ";
                aileron += "/r/n";
                CommandS.Instance.openThread(aileron);
            }
        }

        public float elevatorCommand
        {
            set
            {
                //send the elevator updated value to simulator
                string elevator = "set /controls/flight/elevator ";
                elevator += " ";
                elevator += value;
                elevator += " ";
                elevator += "/r/n";
                CommandS.Instance.openThread(elevator);
            }
        }
    }
}

