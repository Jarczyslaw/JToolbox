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

namespace ToolboxInstaller
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IDialogsService dialogs = new DialogsService();
        private ObservableCollection<ItemViewModel> items = new ObservableCollection<ItemViewModel>();
        private ObservableCollection<ItemViewModel> flatItems = new ObservableCollection<ItemViewModel>();
        private bool windowEnabled = true;
        private string title;
        private bool busy;
        private readonly string startPath = "../../../../Source/";
        private string selectedPath;

        public MainViewModel()
        {
            SetBusy(false);
            //TestData();
            InitData(null, startPath);
        }

        public string SelectedPath
        {
            get => selectedPath;
            set => Set(ref selectedPath, value);
        }

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public bool WindowEnabled
        {
            get => windowEnabled;
            set => Set(ref windowEnabled, value);
        }

        public string ToolboxPath => Path.Combine(SelectedPath, "JToolbox");

        public ObservableCollection<ItemViewModel> Items
        {
            get => items;
            set => Set(ref items, value);
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

        public RelayCommand CloseCommand => new RelayCommand(() => Application.Current.Shutdown());

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

        private async Task FindProjects()
        {
            try
            {
                SetBusy(true, "Reading structure");
                await Task.Run(() =>
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

        private void SetBusy(bool busy, string msg = null)
        {
            this.busy = busy;
            var tempTitle = "Toolbox Installer";
            if (busy)
            {
                tempTitle += " - " + (string.IsNullOrEmpty(msg) ? "BUSY" : msg.ToUpper());
            }
            Title = tempTitle;
            WindowEnabled = !busy;
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

        private void TestData()
        {
            var project11 = new ItemViewModel
            {
                Title = "Project_1_1",
                IsProject = true
            };
            var project12 = new ItemViewModel
            {
                Title = "Project_1_2",
                IsProject = true,
            };
            var project211 = new ItemViewModel
            {
                Title = "Project_2_1_1",
                IsProject = true
            };
            var project212 = new ItemViewModel
            {
                Title = "Project_2_1_2",
                IsProject = true
            };
            var project213 = new ItemViewModel
            {
                Title = "Project_2_1_3",
                IsProject = true
            };

            var project21 = new ItemViewModel
            {
                Title = "Project_2_1",
                IsProject = true
            };
            var project22 = new ItemViewModel
            {
                Title = "Project_2_2",
                IsProject = true
            };

            var folder1 = new ItemViewModel
            {
                Title = "Folder_1",
            };

            var folder2 = new ItemViewModel
            {
                Title = "Folder_2",
            };

            var folder21 = new ItemViewModel
            {
                Title = "Folder_21",
            };

            Items = new ObservableCollection<ItemViewModel>(new List<ItemViewModel>
            {
                folder1.AddChildren(new List<ItemViewModel>
                {
                    project11,
                    project12
                }),
                folder2.AddChildren(new List<ItemViewModel>
                {
                    folder21.AddChildren(new List<ItemViewModel>
                    {
                        project211,
                        project212,
                        project213,
                    }),
                    project21,
                    project22
                }),
            });
        }
    }
}