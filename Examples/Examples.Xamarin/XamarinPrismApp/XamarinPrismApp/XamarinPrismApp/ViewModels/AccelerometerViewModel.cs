﻿using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using System.Numerics;
using Xamarin.Essentials;

namespace XamarinPrismApp.ViewModels
{
    public class AccelerometerViewModel : ViewModelBase
    {
        private readonly IDialogsService dialogsService;
        private string accelerometerOutput;

        public AccelerometerViewModel(IDialogsService dialogsService, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Accelerometer";

            this.dialogsService = dialogsService;
            SetReading(null);
        }

        public string AccelerometerOutput
        {
            get => accelerometerOutput;
            set => SetProperty(ref accelerometerOutput, value);
        }

        public DelegateCommand StartCommand => new DelegateCommand(Start);
        public DelegateCommand StopCommand => new DelegateCommand(Stop);

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            SetReading(e.Reading.Acceleration);
        }

        private void Accelerometer_ShakeDetected(object sender, System.EventArgs e)
        {
            dialogsService.Toast("Shake detected");
        }

        private string GetFloatString(float value)
        {
            return value.ToString("n2");
        }

        private void SetReading(Vector3? reading)
        {
            if (reading == null)
            {
                AccelerometerOutput = "X: n/a Y: n/a Z: n/a";
            }
            else
            {
                var value = reading.Value;
                AccelerometerOutput = $"X: {GetFloatString(value.X)} Y: {GetFloatString(value.Y)} Z: {GetFloatString(value.Z)}";
            }
        }

        private void Start()
        {
            if (Accelerometer.IsMonitoring)
            {
                return;
            }

            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.Start(SensorSpeed.UI);
        }

        private void Stop()
        {
            if (!Accelerometer.IsMonitoring)
            {
                return;
            }

            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.Stop();
            SetReading(null);
        }
    }
}