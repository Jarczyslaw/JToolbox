using JToolbox.Desktop.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Examples.Desktop.Base.ViewModels
{
    public class InputViewModel : BindableBase
    {
        private string label;
        private string text;

        public Action Close { get; set; }

        public DelegateCommand CloseCommand => new DelegateCommand(() => Close?.Invoke());

        public string Label
        {
            get => label;
            set => SetProperty(ref label, value);
        }

        public string Result { get; set; }

        public DelegateCommand SaveCommand => new DelegateCommand(() =>
                                        {
                                            if (ValidationRule != null)
                                            {
                                                var error = ValidationRule(Text);
                                                if (!string.IsNullOrEmpty(error))
                                                {
                                                    new DialogsService().ShowInfo(error);
                                                    return;
                                                }
                                            }

                                            Result = Text;
                                            Close?.Invoke();
                                        });

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public Func<string, string> ValidationRule { get; set; }
    }
}