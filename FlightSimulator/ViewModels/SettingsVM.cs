using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
using FlightSimulator.Views;

namespace FlightSimulator.ViewModels
{
    class SettingsVM
    {
        private ICommand _settingsComand;

        public ICommand SettingsCommand
        {
            get
            {
                return _settingsComand ?? (_settingsComand =
                new CommandHandler(() => OnClick()));
            }
            set
            {

            }
        }
        private void OnClick()
        {
            PopUpSetting setting = new PopUpSetting();
            setting.ShowDialog();
        }

        private ICommand _disConnect;

        public ICommand DisConnectCommand
        {
            get
            {
                return _disConnect ?? (_disConnect =
                new CommandHandler(() => OnClickDisConnect()));
            }
            set
            {

            }
        }
        private void OnClickDisConnect()
        {
            Info.Instance.shouldContinue = false;

            CommandS.Instance.disconnect();
        }

        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand =
                new CommandHandler(() => OnClickCONNECT()));
            }
            set
            {

            }
        }

        private void OnClickCONNECT()
        {
            if (CommandS.Instance.getIsConnected())
            {
                new Thread(() =>
                {
                    CommandS.Instance.disconnect();
                    CommandS.Instance.connect();
                }).Start();

            }
            else
            {
                new Thread(() =>
                {
                    Info.Instance.connect();
                    CommandS.Instance.connect();
                }).Start();
            }
        }
    }
}