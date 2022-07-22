using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;

namespace XamarinPrismApp.ViewModels
{
    public class FileSystemViewModel : ViewModelBase
    {
        private readonly IApplicationCoreService applicationCoreService;
        private readonly IDialogsService dialogsService;
        private string text;

        public FileSystemViewModel(INavigationService navigationService, IDialogsService dialogsService,
            IApplicationCoreService applicationCoreService)
            : base(navigationService)
        {
            Title = "File system";

            this.dialogsService = dialogsService;
            this.applicationCoreService = applicationCoreService;

            Text += $"{nameof(applicationCoreService.InternalFolder)}: {applicationCoreService.InternalFolder}" + Environment.NewLine
                + $"{nameof(applicationCoreService.PrivateExternalFolder)}: {applicationCoreService.PrivateExternalFolder}" + Environment.NewLine
                + $"{nameof(applicationCoreService.PublicExternalFolder)}: {applicationCoreService.PublicExternalFolder}";
        }

        public DelegateCommand RunCommand => new DelegateCommand(Run);

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private void AppendText(string txt)
        {
            if (string.IsNullOrEmpty(Text))
            {
                Text = txt;
            }
            else
            {
                Text = Text + Environment.NewLine + txt;
            }
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void CreateFile(string path)
        {
            File.WriteAllText(path, string.Empty);
        }

        private async void Run()
        {
            var filesCreated = new List<string>();

            try
            {
                Text = string.Empty;

                var paths = new List<string>
                {
                    applicationCoreService.InternalFolder,
                    applicationCoreService.PrivateExternalFolder,
                    applicationCoreService.PublicExternalFolder
                };

                foreach (var path in paths)
                {
                    try
                    {
                        var targetPath = Path.Combine(path, "test.txt");
                        AppendText($"Path: {targetPath}");
                        CreateDirectory(path);
                        CreateFile(targetPath);
                        AppendText("OK");

                        filesCreated.Add(targetPath);
                    }
                    catch (Exception exc)
                    {
                        AppendText("Fail: " + exc.Message);
                    }
                }
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc, null);
            }
            finally
            {
                foreach (var fileCreated in filesCreated)
                {
                    File.Delete(fileCreated);
                }
            }
        }
    }
}