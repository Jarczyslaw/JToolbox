using JToolbox.WPF.Core.Base;
using System.Windows;

namespace ToolboxInstaller
{
    public class ProgressViewModel : BaseViewModel
    {
        private bool isBusy;
        private bool isIndeterminate;
        private int projectsToUpdate;
        private string status;
        private int updatedProjects;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                Set(ref isBusy, value);

                OnPropertyChanged(nameof(ProgressVisibility));
            }
        }

        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set => Set(ref isIndeterminate, value);
        }

        public Visibility ProgressVisibility => IsBusy ? Visibility.Visible : Visibility.Collapsed;

        public int ProjectsToUpdate
        {
            get => projectsToUpdate;
            set => Set(ref projectsToUpdate, value);
        }

        public string Status
        {
            get => status;
            set => Set(ref status, value);
        }

        public int UpdatedProjects
        {
            get => updatedProjects;
            set => Set(ref updatedProjects, value);
        }

        public void IncrementUpdatedProjects()
        {
            UpdatedProjects++;
            RefreshUpdateProjectsStatus();
        }

        public void SetReadingStructure()
        {
            IsBusy = true;
            Status = "Reading structure";
            IsIndeterminate = true;
        }

        public void SetUnbusy()
        {
            IsBusy = false;
        }

        public void SetUpdate(int projectsToUpdate)
        {
            IsBusy = true;
            IsIndeterminate = false;
            ProjectsToUpdate = projectsToUpdate;
            UpdatedProjects = 0;
            RefreshUpdateProjectsStatus();
        }

        private void RefreshUpdateProjectsStatus()
        {
            Status = $"Updating {UpdatedProjects}/{ProjectsToUpdate}";
        }
    }
}