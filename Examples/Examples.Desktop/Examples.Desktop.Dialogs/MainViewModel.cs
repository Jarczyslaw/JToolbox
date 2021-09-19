using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Examples.Desktop.Dialogs
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IDialogsService dialogs = new DialogsService();

        public MainViewModel()
        {
            var filter = new FilterPair
            {
                Extensions = "txt",
                Title = "Text files"
            };
            var filters = new List<FilterPair>
            {
                filter
            };

            DialogActions = new ObservableCollection<DialogActionViewModel>
            {
                new DialogActionViewModel
                {
                    Title = "Info",
                    Action = () => dialogs.ShowInfo("Sample information", "Sample information details")
                },
                new DialogActionViewModel
                {
                    Title = "Warning",
                    Action = () => dialogs.ShowWarning("Sample warning", "Sample warning details")
                },
                new DialogActionViewModel
                {
                    Title = "Error",
                    Action = () => dialogs.ShowError("Sample error", "Sample error details")
                },
                new DialogActionViewModel
                {
                    Title = "Exception",
                    Action = () => dialogs.ShowException(GetException(), "Sample exception")
                },
                new DialogActionViewModel
                {
                    Title = "Critical exception",
                    Action = () => dialogs.ShowCriticalException(GetException(), "Sample critial exception")
                },
                new DialogActionViewModel
                {
                    Title = "Yes/no question",
                    Action = () =>
                    {
                        var result = dialogs.ShowYesNoQuestion("Sample yes no question");
                        dialogs.ShowInfo("Result: " + result);
                    }
                },
                new DialogActionViewModel
                {
                    Title = "Custom question",
                    Action = () =>
                    {
                        var buttons = new List<CustomButton<int>>
                        {
                            new CustomButton<int>
                            {
                                Text = "1",
                                Value = 1
                            },
                            new CustomButton<int>
                            {
                                Text = "2",
                                Value = 2
                            },
                            new CustomButton<int>
                            {
                                Text = "3",
                                Value = 3,
                                Default = true
                            }
                        };
                        var result = dialogs.ShowCustomButtonsQuestion("Custom question", buttons);
                        dialogs.ShowInfo("Result: " + result);
                    }
                },
                new DialogActionViewModel
                {
                    Title = "Open file",
                    Action = () =>
                    {
                        var result = dialogs.OpenFile("Sample open file dialog", false, null, filters);
                        dialogs.ShowInfo("Result: " + result);
                    }
                },
                new DialogActionViewModel
                {
                    Title = "Open files",
                    Action = () =>
                    {
                        var result = dialogs.OpenFiles("Sample open files dialog", false, null, filters);
                        dialogs.ShowInfo("Result: " + result);
                    }
                },
                new DialogActionViewModel
                {
                    Title = "Open folder",
                    Action = () =>
                    {
                        var result = dialogs.OpenFolder("Sample open folder dialog", null);
                        dialogs.ShowInfo("Result: " + result);
                    }
                },
                new DialogActionViewModel
                {
                    Title = "Save file",
                    Action = () =>
                    {
                        var result = dialogs.SaveFile("Sample save file dialog", null, null, filter);
                        dialogs.ShowInfo("Result: " + result);
                    }
                },
            };
            SelectedAction = DialogActions.First();
        }

        public ObservableCollection<DialogActionViewModel> DialogActions { get; set; }
        public RelayCommand OpenCommand => new RelayCommand(() => SelectedAction?.Action());
        public DialogActionViewModel SelectedAction { get; set; }

        private Exception GetException()
        {
            try
            {
                throw new Exception("exc");
            }
            catch (Exception exc)
            {
                return exc;
            }
        }
    }
}