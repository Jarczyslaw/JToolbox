using JToolbox.Core.Extensions;
using JToolbox.Desktop.Dialogs;
using JToolbox.Threading;
using JToolbox.WPF.Core;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Examples.Desktop.Base.ViewModels
{
    public class MainViewModel : BindableBase, IOutputInput
    {
        private readonly QueuedLock queuedLock = new QueuedLock();
        private bool busy;
        private string title;
        private string messages;
        private ExampleViewModel selectedExample;
        private DelegateCommand runCommand;
        private IDialogsService dialogsService = new DialogsService();
        private Stopwatch internalStopwatch;

        public MainViewModel()
        {
            var examples = Assembly.GetEntryAssembly()
                .GetTypesImplements<IDesktopExample>()
                .Select(s => (IDesktopExample)Activator.CreateInstance(s))
                .OrderBy(e => e.Title)
                .ToList();
            InitializeExamples(examples);
        }

        public DelegateCommand RunCommand => runCommand ?? (runCommand = new DelegateCommand(async () =>
        {
            Busy = true;
            Clear();
            WriteLine($"{SelectedExample.Display} started...");
            try
            {
                var stopwatch = Stopwatch.StartNew();
                await Task.Run(() => SelectedExample.Example.Run(this));
                WriteLine($"{SelectedExample.Display} finished successfully in {Math.Round(stopwatch.Elapsed.TotalMilliseconds)}ms");
            }
            catch (Exception exc)
            {
                WriteLine($"{SelectedExample.Display} failed with exception:");
                WriteLine(exc.ToString());
            }
            finally
            {
                Busy = false;
            }
        }, () => SelectedExample != null && !Busy));

        public DelegateCommand NewWindowCommand => new DelegateCommand(() => WindowManager.GetMainWindow(Title).Show());

        public bool Busy
        {
            get => busy;
            set
            {
                SetProperty(ref busy, value);
                RunCommand.RaiseCanExecuteChanged();
            }
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public ExampleViewModel SelectedExample
        {
            get => selectedExample;
            set
            {
                SetProperty(ref selectedExample, value);
                RunCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<ExampleViewModel> Examples { get; set; } = new ObservableCollection<ExampleViewModel>();

        public string Messages
        {
            get => messages;
            set => SetProperty(ref messages, value);
        }

        public string Read(string label, string text = null)
        {
            string result = null;
            Threading.SafeInvoke(() => result = WindowManager.GetInput(label, text));
            return result;
        }

        public void Write(string message)
        {
            queuedLock.LockedAction(() => Messages += message);
        }

        public void WriteLine(string message)
        {
            queuedLock.LockedAction(() => Messages += message + Environment.NewLine);
        }

        public void PutLine()
        {
            WriteLine(string.Empty);
        }

        public void Clear()
        {
            queuedLock.LockedAction(() => Messages = string.Empty);
        }

        public void Wait(string message)
        {
            Threading.SafeInvoke(() => dialogsService.ShowInfo(message));
        }

        private void InitializeExamples(List<IDesktopExample> examples)
        {
            Examples = new ObservableCollection<ExampleViewModel>(examples.Select(e => new ExampleViewModel
            {
                Example = e
            }));
            SelectedExample = Examples.FirstOrDefault();
        }

        public async Task CleanUp()
        {
            foreach (var example in Examples)
            {
                await example.Example.CleanUp();
            }
        }

        public void StartTime()
        {
            internalStopwatch = Stopwatch.StartNew();
            WriteLine("Stopwatch started");
        }

        public void StopTime()
        {
            WriteLine($"Elapsed time: {Math.Round(internalStopwatch.Elapsed.TotalMilliseconds)}ms");
        }
    }
}