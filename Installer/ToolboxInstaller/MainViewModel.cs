using JToolbox.WPF.Core.Base;
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
        private ObservableCollection<ItemViewModel> items = new ObservableCollection<ItemViewModel>();
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

        public ObservableCollection<ItemViewModel> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        public RelayCommand SelectPathCommand => new RelayCommand(() =>
        {
        });

        public RelayCommand UpdateCommand => new RelayCommand(async () =>
        {
            try
            {
                SetBusy(true);
                await Task.Delay(2000);
            }
            finally
            {
                SetBusy(false);
            }
        });

        private bool UpdateCanExecute()
        {
            var checkedItems = new List<ItemViewModel>();
            GetCheckedItems(checkedItems);
            return busy && checkedItems.Any(s => s.IsChecked);
        }

        private void GetCheckedItems(List<ItemViewModel> checkedItems)
        {
            foreach (var item in Items)
            {
                if (item.IsChecked)
                {
                    checkedItems.Add(item);
                }

                GetCheckedItems(checkedItems);
            }
        }

        public RelayCommand CloseCommand => new RelayCommand(() => Application.Current.Shutdown());

        private void SetBusy(bool busy)
        {
            this.busy = busy;
            var tempTitle = "Toolbox Installer";
            if (busy)
            {
                tempTitle += " - INSTALLING";
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