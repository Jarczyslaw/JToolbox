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
        private bool busy;
        private string title;
        private string messages;
        private ExampleViewModel selectedExample;
        private DelegateCommand runCommand;
        private IDialogsService dialogsService = new DialogsService();
        private Stopwatch internalStopwatch;
        private ProducerConsumer<string> messagesProxy = new ProducerConsumer<string>();

        public MainViewModel()
        {
            var examples = Assembly.GetEntryAssembly()
                .GetTypesImplements<IDesktopExample>()
                .Select(s => (IDesktopExample)Activator.CreateInstance(s))
                .OrderBy(e => e.Title)
                .ToList();
            InitializeExamples(examples);
            messagesProxy.Handler += s =>
            {
                if (s == null)
                {
                    Messages = string.Empty;
                }
                else
                {
                    Messages += s;
                }
                return Task.CompletedTask;
            };
        }

        public DelegateCommand RunCommand => runCommand ?? (runCommand = new DelegateCommand(async () =>
        {
            Busy = true;
            Clear();
            WriteLine($"[MAIN] {SelectedExample.Display} started...");
            try
            {
                var stopwatch = Stopwatch.StartNew();
                await Task.Run(() => SelectedExample.Example.Run(this));
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

        public string Read(string label, string text = null, Func<string, string> validationRule = null)
        {
            string result = null;
            Threading.SafeInvoke(() => result = WindowManager.GetInput(label, text, validationRule));
            return result;
        }

        public T SelectValue<T>(string label, List<T> values)
        {
            T result = default;
            Threading.SafeInvoke(() => result = WindowManager.SelectValue(label, values));
            return result;
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

        public void PutLine()
        {
            WriteLine(string.Empty);
        }

        public void Clear()
        {
            messagesProxy.Add(null);
        }

        public void Wait()
        {
            Threading.SafeInvoke(() => dialogsService.ShowInfo("Click OK to continue..."));
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
            await messagesProxy.Cancel();
            foreach (var example in Examples)
            {
                await example.Example.CleanUp();
            }
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
    }
}