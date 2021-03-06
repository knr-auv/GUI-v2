﻿using GUI_v2.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_v2.ViewModel
{
    public partial class AutonomyViewModel : BaseViewModel
    {
        public LoggerViewModel LoggerViewModel { get; set; }

        public CameraStreamViewModel CameraViewModel { get; set; }
        public DetectionListViewModel DetectionListViewModel { get; set; }

        public bool JetsonConnected
        {
            get { return modelContainer.modelStatus.networkStatus.ConnectedToJetson; }
            set { RaisePropertyChanged("JetsonConnected"); }
        }

        public bool AutonomyStatus
        {
            get { return modelContainer.modelStatus.AutonomyRunning; }
            set { RaisePropertyChanged("AutonomyStatus"); }
        }
        public Dictionary<string, string> TaskManagerStatus
        {
            get { return modelContainer.modelStatus.taskManagerStatus.Status; }
            set { RaisePropertyChanged("TaskManagerStatus"); }
        }
        public float[] MotorsData
        {
            get { return modelContainer.dataContainer.motorsData.Data; }
            set { RaisePropertyChanged("MotorsData"); }
        }
        public double Roll
        {
            get { return -modelContainer.dataContainer.Attitude.x; }
            set { RaisePropertyChanged("Roll"); }
        }
//positive rotation is clockwise...
        public double Pitch
        {
            get { return modelContainer.dataContainer.Attitude.y; }
            set { RaisePropertyChanged("Pitch"); }
        }
        public double Heading
        {
            get { return modelContainer.dataContainer.Attitude.z; }
            set { RaisePropertyChanged("Heading"); }
        }
        public double Acceleration
        {
            get { return modelContainer.dataContainer.Acc.norm; }
            set { RaisePropertyChanged("Acceleration"); }
        }
        public double Velocity
        {
            get { return modelContainer.dataContainer.Velocity.norm; }
            set { RaisePropertyChanged("Velocity"); }
        }
        public double posX {
            get { return modelContainer.dataContainer.Position.x; }
            set { RaisePropertyChanged("posX"); }
        }
        public double posY
        {
            get { return modelContainer.dataContainer.Position.y; }
            set { RaisePropertyChanged("posY"); }
        }
        public double posZ
        {
            get { return modelContainer.dataContainer.Position.z; }
            set { RaisePropertyChanged("posZ"); }
        }
    }
}
