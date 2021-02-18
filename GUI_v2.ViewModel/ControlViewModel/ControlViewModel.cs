﻿using GUI_v2.Tools;
using GUI_v2.ViewModel.Commands;
using GUI_v2.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel
{
    public partial class ControlViewModel : BaseViewModel
    {
        public override void Hide()
        {
            modelContainer.dataContainer.Position.newDataCallback -= Position.UpdateInfo;
            modelContainer.dataContainer.Velocity.newNormCallback -= UpdateVelocity;
            modelContainer.dataContainer.Acceleration.newNormCallback -= UpdateAcceleration;
            modelContainer.dataContainer.Attitude.newDataCallback -=UpdateAttitude;
        }
        public override void Show()
        {
            modelContainer.cameraStreamClient.StartStream(CameraViewModel.SwapImage, CameraViewModel.SetLogo);
            modelContainer.dataContainer.Position.newDataCallback += Position.UpdateInfo;
            modelContainer.dataContainer.Velocity.newNormCallback += UpdateVelocity;
            modelContainer.dataContainer.Acceleration.newNormCallback += UpdateAcceleration;
            modelContainer.dataContainer.Attitude.newDataCallback +=UpdateAttitude;

        }

        
        public ModelContainer modelContainer;

        public ControlViewModel(ModelContainer modelContainer)
        {
            Position = new MovementInfoClass();
            this.modelContainer = modelContainer;
            CameraViewModel = new CameraStreamViewModel();
            HUDViewModel = new HUDViewModel();
            DetectionListViewModel = new DetectionListViewModel(modelContainer);
            DetectionDrawerViewModel = new DetectionDrawerViewModel(modelContainer);
            ModeChangedCommand = new RelayCommand(ModeChangedAction, CanArmAction);
            ArmCommand = new RelayCommand(ArmAction, CanArmAction);
            DisarmCommand = new RelayCommand(DisarmAction, CanDisarmAction);
            DetectionBtnClickedCommand = new RelayCommand(DetectionBtnClickedAction, (object p) => { return true; });
            modelContainer.modelStatus.ArmCallback += (bool value) => { Armed = value; };
            modelContainer.modelStatus.networkStatus.ConnectedToJetsonCallback += (bool value) => { JetsonConnected = value; };
            modelContainer.modelStatus.networkStatus.CameraStreamConnectedCallback += CameraStreamStatusChangedCallback;
            modelContainer.modelStatus.DetectorStatusCallback += (bool val) =>
            {
                DetectionState = val;
              
            };
        }

        private void UpdateVelocity(float x) {Velocity = x;}
        private void UpdateAcceleration(float x) { Acceleration = x; }
        private void UpdateAttitude(float x, float y, float z) { HUDViewModel.UpdateAttitude(x, y, z); }
        private void CameraStreamStatusChangedCallback(bool val)
        {
            if (val == false)
            {
                CameraViewModel.SetLogo();
            }
        }
    }
}
