using Examples.Desktop.Base.ViewModels;
using Examples.Desktop.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples.Desktop.Base
{
    public static class WindowManager
    {
        public static MainWindow GetMainWindow(string title)
        {
            var viewModel = new MainViewModel
            {
                Title = title
            };
            return new MainWindow(viewModel);
        }

        public static string GetInput(string label, string text = null, Func<string, string> validationRule = null)
        {
            var viewModel = new InputViewModel
            {
                Text = text,
                Label = label,
                ValidationRule = validationRule
            };
            var window = new InputWindow(viewModel);
            window.ShowDialog();
            return viewModel.Result;
        }

        public static T SelectValue<T>(string label, List<T> values)
        {
            var viewModel = new SelectViewModel(values.Cast<object>().ToList())
            {
                Label = label
            };
            var window = new SelectWindow(viewModel);
            window.ShowDialog();
            return (T)viewModel.Result;
        }
    }
}