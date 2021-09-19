using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Awareness.Args;
using JToolbox.WPF.Core.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Examples.Desktop.WPFDragDrop.ViewModels
{
    public class FilesContextViewModel : BaseViewModel, IFileDragDropAware
    {
        private ObservableCollection<string> files = new ObservableCollection<string>();
        private string selectedFile;

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

        public void OnFileDrag(FileDragArgs args)
        {
            var newFileName = Path.GetFileNameWithoutExtension(selectedFile) + "_bek.txt";
            var newFile = Path.Combine(Path.GetTempPath(), newFileName);
            File.WriteAllText(newFile, "TEST FILE");
            args.Files = new List<string> { newFile };
            EventLogs.AddWithClassName("File drag: " + newFile);
        }

        public void OnFilesDrop(FileDropArgs args)
        {
            foreach (var filePath in args.Files)
            {
                Files.Add(filePath);
            }
            EventLogs.AddWithClassName("Files drop: " + string.Join(", ", args.Files.ToArray()));
        }
    }
}