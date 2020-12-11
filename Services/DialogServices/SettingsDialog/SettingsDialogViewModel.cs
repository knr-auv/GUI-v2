﻿using GUI_v2.Model.UserSettings;
using GUI_v2.Services;
using GUI_v2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Services.DialogServices.SettingsDialog
{
    public class SettingsDialogViewModel:DialogBaseViewModel
    {
        public ControlSettingsViewModel ControlSettingsViewModel { get; set; }
        private bool closeClicked = false;
        private UserSettings userSettings;

        public ICommand SaveCommand { get; set; }
        private void SaveAction(object param)
        {
            userSettings.Save("UserSettings.xaml");//UserSettings
            closeClicked = true;
            CloseDialogWithResult(param as Window, DialogResult.Undefined);
        }

        public void CloseAction(object parameter)
        {
            var i = (CancelEventArgs)parameter;
            if (closeClicked)
                i.Cancel = false;
            else
                i.Cancel = true;
        }
       


        private bool allow(object p)
        {
            return true;
        }

        public ICommand CloseDialog { get; private set; }
        public void OnCloseAction(object parameter)
        {
            closeClicked = true;
            CloseDialogWithResult(parameter as Window, DialogResult.Undefined);
        }

        public string JetsonIP
        {
            get { return userSettings.networkSettings.JetsonIP; }
            set { userSettings.networkSettings.JetsonIP = value; }
        }
        public int ControlPort
        {
            get { return userSettings.networkSettings.ControlPort; }
            set { userSettings.networkSettings.ControlPort = value; }
        }
        public int VideoStreamPort
        {
            get { return userSettings.networkSettings.VideoStreamPort; }
            set { userSettings.networkSettings.VideoStreamPort = value; }
        }


        public SettingsDialogViewModel(ModelContainer modelConatiner)
        {
            userSettings = modelConatiner.userSettings;
            CloseDialog = new RelayCommand(OnCloseAction, allow);
            Closing = new RelayCommand(CloseAction, allow);
            SaveCommand = new RelayCommand(SaveAction, allow);
            ControlSettingsViewModel = new ControlSettingsViewModel(userSettings.ControlSettings);
        }


    }
}
