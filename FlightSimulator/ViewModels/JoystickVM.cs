using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class JoystickVM : BaseNotify
    {
        //string[] input;
        string line;
        public JoystickVM()
        {
            line = "";
        }

        public string Line
        {
         
            set
            {
                string line = value;
               

            }
        }

        private string to_string(string value)
        {
            throw new NotImplementedException();
        }

        private ICommand _sliderRudder;
        public ICommand rudderCommand
        {
            get
            {
                return _sliderRudder ?? (_sliderRudder = new CommandHandler(() => slideRudder()));
            }
        }
        private void slideRudder()
        {
            string rudder = "set controls/flight/rudder ";
            rudder += line;
            rudder += "/r/n";
            //CommandS command = new CommandS();
            CommandS.Instance.openThread(rudder);
        }

        private ICommand _sliderThrottle;
        public ICommand throttleCommand
        {
            get
            {
                return _sliderThrottle ?? (_sliderThrottle = new CommandHandler(() => slideThrottle()));
            }
        }
        private void slideThrottle()
        {
            string throttle = "controls/engines/current-engine/throttle ";
            throttle += line;
            throttle += "/r/n";
            //CommandS command = new CommandS();
            CommandS.Instance.openThread(throttle);
        }
    }
}

