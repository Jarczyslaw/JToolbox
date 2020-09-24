using JToolbox.WPF.Core.Base;
using System;

namespace WpfMvvmDragDrop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string logs = string.Empty;

        public MainViewModel()
        {
            EventLogs.AddEvent += AddEvent;
        }

        public TabsContextViewModel TabsContext { get; set; } = new TabsContextViewModel();
        public FilesContextViewModel FilesContext { get; set; } = new FilesContextViewModel();

        public RelayCommand ClearCommand => new RelayCommand(() => Logs = string.Empty);

        public string Logs
        {
            get => logs;
            set => Set(ref logs, value);
        }

        private void AddEvent(string log)
        {
            Logs = Logs + (string.IsNullOrEmpty(Logs) ? string.Empty : Environment.NewLine) + log;
        }
    }
}