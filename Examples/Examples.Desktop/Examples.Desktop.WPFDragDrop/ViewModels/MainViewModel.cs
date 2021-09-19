using JToolbox.WPF.Core.Base;
using System;

namespace Examples.Desktop.WPFDragDrop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string logs = string.Empty;

        public MainViewModel()
        {
            EventLogs.AddEvent += AddEvent;
        }

        public RelayCommand ClearCommand => new RelayCommand(() => Logs = string.Empty);
        public FilesContextViewModel FilesContext { get; set; } = new FilesContextViewModel();

        public string Logs
        {
            get => logs;
            set => Set(ref logs, value);
        }

        public TabsContextViewModel TabsContext { get; set; } = new TabsContextViewModel();

        private void AddEvent(string log)
        {
            Logs = Logs + (string.IsNullOrEmpty(Logs) ? string.Empty : Environment.NewLine) + log;
        }
    }
}