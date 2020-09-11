using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfMvvmDragDrop.ViewModels
{
    public class FilesContextViewModel : BaseViewModel, IFileDragDropAware
    {
        private string selectedFile;
        private ObservableCollection<string> files = new ObservableCollection<string>();

        public ObservableCollection<string> Files
        {
            get => files;
            set => Set(ref files, value);
        }

        public string SelectedFile
        {
            get => selectedFile;
            set => Set(ref selectedFile, value);
        }

        public List<string> OnFileDrag()
        {
            var newFileName = Path.GetFileNameWithoutExtension(selectedFile) + "_bek" + Path.GetExtension(SelectedFile);
            var newFile = Path.Combine(Path.GetTempPath(), newFileName);
            var stream = File.Create(newFile);
            stream.Dispose();
            return new List<string> { newFile };
        }

        public void OnFilesDrop(List<string> filesPaths)
        {
            foreach (var filePath in filesPaths)
            {
                Files.Add(filePath);
            }
        }
    }
}