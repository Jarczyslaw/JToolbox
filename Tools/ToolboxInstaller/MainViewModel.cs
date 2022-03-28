using JToolbox.Core.Helpers;
using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Base;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToolboxInstaller.Properties;

namespace ToolboxInstaller
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IDialogsService dialogs = new DialogsService();
        private readonly ObservableCollection<ItemViewModel> flatItems = new ObservableCollection<ItemViewModel>();
        private readonly string startPath = "../../../../Source/";
        private ObservableCollection<ItemViewModel> items = new ObservableCollection<ItemViewModel>();
        private string selectedPath;
        private string title;
        private bool windowEnabled = true;

        public MainViewModel()
        {
            SetBusy(false);
            InitData(null, startPath);
            RestorePreviousInstalledPath();
        }

        public RelayCommand CloseCommand => new RelayCommand(() => Application.Current.Shutdown());

        public ObservableCollection<ItemViewModel> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        public string SelectedPath
        {
            get => selectedPath;
            set => Set(ref selectedPath, value);
        }

        public RelayCommand SelectPathCommand => new RelayCommand(async () =>
              {
                  var solutionPath = dialogs.OpenFolder("Select solution location");
                  if (!string.IsNullOrEmpty(solutionPath))
                  {
                      SelectedPath = solutionPath;
                      await FindProjects();
                  }
              });

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public string ToolboxPath => Path.Combine(SelectedPath, "JToolbox");

        public RelayCommand UpdateCommand => new RelayCommand(async () =>
              {
                  if (string.IsNullOrEmpty(SelectedPath))
                  {
                      dialogs.ShowError("No target path selected");
                      return;
                  }

                  if (!flatItems.Any(i => i.IsProject && i.IsChecked))
                  {
                      dialogs.ShowError("No projects selected");
                      return;
                  }

                  await UpdateStructure();
              });

        public bool WindowEnabled
        {
            get => windowEnabled;
            set => Set(ref windowEnabled, value);
        }

        private async Task FindProjects()
        {
            try
            {
                SetBusy(true, "Reading structure");
                await Task.Run(() =>
                {
                    foreach (var item in flatItems)
                    {
                        item.SetChecked(false, false);
                    }

                    if (Directory.Exists(ToolboxPath))
                    {
                        var projects = Directory.GetFiles(ToolboxPath, "*.csproj", SearchOption.AllDirectories)
                            .Select(s => Path.GetFileNameWithoutExtension(s));
                        foreach (var item in flatItems)
                        {
                            item.SetChecked(false, true);
                            if (item.IsProject && projects.Contains(item.Title))
                            {
                                item.SetChecked(true, true);
                            }
                        }
                    }
                });
            }
            catch (Exception exc)
            {
                SetBusy(false);
                dialogs.ShowException(exc);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void InitData(ItemViewModel parent, string path)
        {
            var content = Directory.GetDirectories(path, "*.*").ToList();
            foreach (var folder in content)
            {
                var info = new DirectoryInfo(folder);
                var item = new ItemViewModel
                {
                    Title = info.Name,
                    SourcePath = info.FullName,
                    IsExpanded = true,
                };
                item.IsProject = Directory.GetFiles(folder, "*.csproj")?.Length > 0;

                flatItems.Add(item);
                if (parent == null)
                {
                    Items.Add(item);
                }
                else
                {
                    parent.AddChild(item);
                }

                if (!item.IsProject)
                {
                    InitData(item, folder);
                }
            }
        }

        private Task RebuildStructure()
        {
            return Task.Run(() =>
            {
                var projects = flatItems.Where(f => f.IsProject && f.IsChecked);
                foreach (var project in projects)
                {
                    var directoryPath = Path.Combine(ToolboxPath, project.GetPath());
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var source = new DirectoryInfo(project.SourcePath);
                    var target = new DirectoryInfo(Path.Combine(directoryPath, project.Title));
                    FileHelper.CopyAll(source, target);
                }
            });
        }

        private async void RestorePreviousInstalledPath()
        {
            var path = Settings.Default.PreviousInstalledPath;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                SelectedPath = path;
                await FindProjects();
            }
        }

        private void SetBusy(bool busy, string msg = null)
        {
            var tempTitle = "Toolbox Installer";
            if (busy)
            {
                tempTitle += " - " + (string.IsNullOrEmpty(msg) ? "BUSY" : msg.ToUpper());
            }
            Title = tempTitle;
            WindowEnabled = !busy;
        }

        private async Task UpdateStructure()
        {
            try
            {
                SetBusy(true, "Updating");
                var toolboxPath = Path.Combine(SelectedPath, "JToolbox");
                if (Directory.Exists(toolboxPath))
                {
                    Directory.Delete(toolboxPath, true);
                }
                await RebuildStructure();
                dialogs.ShowInfo("Updated!");

                Settings.Default.PreviousInstalledPath = SelectedPath;
                Settings.Default.Save();
            }
            catch (Exception exc)
            {
                SetBusy(false);
                dialogs.ShowException(exc);
            }
            finally
            {
                SetBusy(false);
            }
        }
    }
}