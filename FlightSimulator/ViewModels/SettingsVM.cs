using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Info connectRead = new Info();
            //CommandS connect = new CommandS();
            CommandS.Instance.connect();
        }
    }
}