using JToolbox.Core.Extensions;
using JToolbox.Desktop.Dialogs;
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
        private bool busy;
        private DelegateCommand continueCommand;
        private TaskCompletionSource<object> continueTaskCompletionSource;
        private Stopwatch internalStopwatch;
        private string messages;
        private MessagesProxy messagesProxy;
        private DelegateCommand runCommand;
        private ExampleViewModel selectedExample;
        private string title;
        private List<IDesktopExample> toCleanup = new List<IDesktopExample>();

        public MainViewModel()
        {
            var examples = Assembly.GetEntryAssembly()
                .GetTypesImplements<IDesktopExample>()
                .Select(s => (IDesktopExample)Activator.CreateInstance(s))
                .OrderBy(e => e.Title)
                .ToList();
            InitializeExamples(examples);
            messagesProxy = new MessagesProxy(this);
        }

        public bool Busy
        {
            get => busy;
            set
            {
                SetProperty(ref busy, value);
                RunCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand ContinueCommand => continueCommand ?? (continueCommand = new DelegateCommand(() =>
        {
            continueTaskCompletionSource.SetResult(null);
        }, () => continueTaskCompletionSource != null));

        public ObservableCollection<ExampleViewModel> Examples { get; set; } = new ObservableCollection<ExampleViewModel>();

        public string Messages
        {
            get => messages;
            set => SetProperty(ref messages, value);
        }

        public DelegateCommand NewWindowCommand => new DelegateCommand(() => WindowManager.GetMainWindow(Title).Show());

        public DelegateCommand RunCommand => runCommand ?? (runCommand = new DelegateCommand(async () =>
        {
            Busy = true;
            Clear();
            WriteLine($"[MAIN] {SelectedExample.Display} started...");
            try
            {
                var example = SelectedExample.Example;
                if (!toCleanup.Contains(example))
                {
                    toCleanup.Add(example);
                }

                var stopwatch = Stopwatch.StartNew();
                await Task.Run(() => example.Run(this));
                WriteLine($"[MAIN] {SelectedExample.Display} finished successfully in {Math.Round(stopwatch.Elapsed.TotalMilliseconds)}ms");
            }
            catch (Exception exc)
            {
                WriteLine($"[MAIN] {SelectedExample.Display} failed with exception:");
                WriteLine(exc.ToString());
            }
            finally
            {
                Busy = false;
            }
        }, () => SelectedExample != null && !Busy));

        public ExampleViewModel SelectedExample
        {
            get => selectedExample;
            set
            {
                SetProperty(ref selectedExample, value);
                RunCommand.RaiseCanExecuteChanged();
            }
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public bool CheckClose()
        {
            if (Busy)
            {
                var dialogs = new DialogsService();
                return dialogs.ShowYesNoQuestion("Example is running. Do you want to force close?");
            }
            return true;
        }

        public async Task CleanUp()
        {
            await messagesProxy.Cancel();
            foreach (var example in Examples)
            {
                await example.Example.CleanUp();
            }
        }

        public void Clear()
        {
            messagesProxy.Add(null);
        }

        public void PutLine()
        {
            WriteLine(string.Empty);
        }

        public string Read(string label, string text = null, Func<string, string> validationRule = null)
        {
            string result = null;
            Threading.SafeInvoke(() => result = WindowManager.GetInput(this, label, text, validationRule));
            return result;
        }

        public T SelectValue<T>(string label, List<T> values)
        {
            T result = default;
            Threading.SafeInvoke(() => result = WindowManager.SelectValue(this, label, values));
            return result;
        }

        public void StartTime()
        {
            internalStopwatch = Stopwatch.StartNew();
            WriteLine("[MAIN] Stopwatch started");
        }

        public void StopTime()
        {
            WriteLine($"[MAIN] Elapsed time: {Math.Round(internalStopwatch.Elapsed.TotalMilliseconds)}ms");
        }

        public async Task Wait()
        {
            WriteLine("[MAIN] Waiting for continuation...");
            continueTaskCompletionSource = new TaskCompletionSource<object>();
            ContinueCommand.RaiseCanExecuteChanged();
            await continueTaskCompletionSource.Task;
            continueTaskCompletionSource = null;
            ContinueCommand.RaiseCanExecuteChanged();
        }

        public void Write(string message)
        {
            if (message != null)
            {
                messagesProxy.Add(message);
            }
        }

        public void WriteLine(string message)
        {
            if (message != null)
            {
                messagesProxy.Add(message + Environment.NewLine);
            }
        }

        private void InitializeExamples(List<IDesktopExample> examples)
        {
            Examples = new ObservableCollection<ExampleViewModel>(examples.Select(e => new ExampleViewModel
            {
                Example = e
            }));
            SelectedExample = Examples.FirstOrDefault();
        }
    }
}