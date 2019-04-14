﻿using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotVM : BaseNotify
    {
        //string[] input;
        string line;
        public AutoPilotVM()
        {
            line = "";
        }

        public string Line
        {
            get
            {
                // change color if needed
                NotifyPropertyChanged("Color");
                return line;
            }
            set
            {
                line = value;
            }
        }

        public string Color
        {
            get
            {
                if (line == "")
                {
                    return "White";
                }
                else
                {
                    return "Pink";
                }
            }
            private set { }
        }
        private ICommand _OKCommand;
        public ICommand ClickOKCommand
        {
            get
            {
                return _OKCommand ?? (_OKCommand = new CommandHandler(() => OnClickOK()));
            }
        }
        private void OnClickOK()
        {

            //CommandS command = new CommandS();
            CommandS.Instance.openThread(line);
        }

        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => ClearClick()));
            }
        }
        private void ClearClick()
        {
            line = "";
            NotifyPropertyChanged(line);
        }
    }
}
