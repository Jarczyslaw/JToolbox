using Examples.Desktop.Base.ViewModels;
using Examples.Desktop.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Examples.Desktop.Base
{
    public static class WindowManager
    {
        public static string GetInput(MainViewModel parentViewModel, string label, string text = null, Func<string, string> validationRule = null)
        {
            var viewModel = new InputViewModel
            {
                Text = text,
                Label = label,
                ValidationRule = validationRule
            };
            var window = new InputWindow(viewModel)
            {
                Owner = FindWindow(parentViewModel)
            };
            window.ShowDialog();
            return viewModel.Result;
        }

        public static MainWindow GetMainWindow(string title)
        {
            var viewModel = new MainViewModel
            {
                Title = title
            };
            return new MainWindow(viewModel);
        }

        public static T SelectValue<T>(MainViewModel parentViewModel, string label, List<T> values)
        {
            var viewModel = new SelectViewModel(values.Cast<object>().ToList())
            {
                Label = label
            };
            var window = new SelectWindow(viewModel)
            {
                Owner = FindWindow(parentViewModel)
            };
            window.ShowDialog();
            return (T)viewModel.Result;
        }

        private static Window FindWindow(object dataContext)
        {
            return Application.Current.Windows
                .OfType<Window>()
                .First(w => w.DataContext == dataContext);
        }
    }
}