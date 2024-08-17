using JToolbox.Core;
using JToolbox.Core.Helpers;
using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Base;
using System;
using System.Collections.Generic;
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

        public MainViewModel()
        {
            InitData(null, startPath);
            RestorePreviousInstalledPath();
        }

        public RelayCommand CloseCommand => new RelayCommand(() =>
        {
            if (ProgressViewModel.IsBusy) { return; }

            Application.Current.Shutdown();
        });

        public ObservableCollection<ItemViewModel> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        public ProgressViewModel ProgressViewModel { get; set; } = new ProgressViewModel();

        public RelayCommand SelectAllLoadedCommand => new RelayCommand(() =>
        {
            foreach (var item in flatItems.Where(x => x.IsInstalled))
            {
                item.IsChecked = true;
            }
        });

        public string SelectedPath
        {
            get => selectedPath;
            set => Set(ref selectedPath, value);
        }

        public RelayCommand SelectPathCommand => new RelayCommand(async () =>
        {
            if (ProgressViewModel.IsBusy) { return; }

            var solutionPath = dialogs.OpenFolder("Select solution location");
            if (!string.IsNullOrEmpty(solutionPath))
            {
                SelectedPath = solutionPath;
                await FindProjects();
            }
        });

        public int ToDeleteCount { get; set; }

        public int ToInstallCount { get; set; }

        public string ToolboxPath => Path.Combine(SelectedPath, "JToolbox");

        public int ToUpdateCount { get; set; }

        public RelayCommand UpdateCommand => new RelayCommand(async () =>
        {
            if (ProgressViewModel.IsBusy) { return; }

            if (string.IsNullOrEmpty(SelectedPath))
            {
                dialogs.ShowError("No target path selected");
                return;
            }

            if (!flatItems.Any(i => i.IsToModify))
            {
                dialogs.ShowError("No projects to update");
                return;
            }

            await UpdateStructure();
        });

        public void UpdateItemsCheckedState()
        {
            ToUpdateCount = flatItems.Count(x => x.IsToUpdate);
            ToInstallCount = flatItems.Count(x => x.IsToInstall);
            ToDeleteCount = flatItems.Count(x => x.IsToDelete);

            OnPropertyChanged(nameof(ToUpdateCount));
            OnPropertyChanged(nameof(ToInstallCount));
            OnPropertyChanged(nameof(ToDeleteCount));
        }

        private async Task FindProjects()
        {
            try
            {
                ProgressViewModel.SetReadingStructure();
                await Task.Run(() =>
                {
                    foreach (var item in flatItems)
                    {
                        item.TargetPath = null;
                        item.IsInstalled = false;
                        item.SetChecked(false, false);
                    }

                    if (!Directory.Exists(ToolboxPath)) { return; }

                    Dictionary<string, string> projects = Directory.GetFiles(ToolboxPath, "*.csproj", SearchOption.AllDirectories)
                        .ToDictionary(x => Path.GetFileNameWithoutExtension(x));

                    foreach (var item in flatItems)
                    {
                        item.SetChecked(false, true);

                        if (item.IsProject && projects.TryGetValue(item.Title, out string targetPath))
                        {
                            item.TargetPath = Path.GetDirectoryName(targetPath);
                            item.IsInstalled = true;
                            item.SetChecked(null, true);
                        }
                    }
                });
            }
            catch (Exception exc)
            {
                ProgressViewModel.SetUnbusy();
                dialogs.ShowException(exc);
            }
            finally
            {
                ProgressViewModel.SetUnbusy();
            }
        }

        private void InitData(ItemViewModel parent, string path)
        {
            var content = Directory.GetDirectories(path, "*.*").ToList();
            foreach (var folder in content)
            {
                var info = new DirectoryInfo(folder);
                var item = new ItemViewModel(this)
                {
                    Title = info.Name,
                    SourcePath = info.FullName,
                    IsExpanded = true,
                    IsProject = Directory.GetFiles(folder, "*.csproj")?.Length > 0
                };

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

        private Task RebuildStructure(List<ItemViewModel> toUpdate)
        {
            return Task.Run(() =>
            {
                foreach (ItemViewModel project in toUpdate)
                {
                    if (Directory.Exists(project.TargetPath)) { Directory.Delete(project.TargetPath, true); }

                    if (project.IsToDelete) { continue; }

                    DirectoryInfo sourcePath = new DirectoryInfo(project.SourcePath);

                    DirectoryInfo targetPath = null;
                    if (project.IsToInstall)
                    {
                        var directoryPath = Path.Combine(ToolboxPath, project.GetPath());
                        targetPath = new DirectoryInfo(Path.Combine(directoryPath, project.Title));
                    }
                    else if (project.IsToUpdate)
                    {
                        targetPath = new DirectoryInfo(project.TargetPath);
                    }

                    if (!Directory.Exists(targetPath.FullName)) { Directory.CreateDirectory(targetPath.FullName); }

                    FileSystemHelper.CopyAll(sourcePath, targetPath);

                    ProgressViewModel.IncrementUpdatedProjects();
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

        private async Task UpdateStructure()
        {
            try
            {
                List<ItemViewModel> toUpdate = flatItems.Where(x => x.IsToModify)
                    .ToList();

                ProgressViewModel.SetUpdate(toUpdate.Count);
                await RebuildStructure(toUpdate);
                await Task.Delay(200);
                dialogs.ShowInfo("Updated!");

                Settings.Default.PreviousInstalledPath = SelectedPath;
                Settings.Default.Save();
            }
            catch (Exception exc)
            {
                ProgressViewModel.SetUnbusy();
                dialogs.ShowException(exc);
            }
            finally
            {
                ProgressViewModel.SetUnbusy();
                await FindProjects();
            }
        }
    }
}