using System.Collections.Generic;

namespace JToolbox.WPF.Core.Awareness
{
    public interface IFileDropAware
    {
        void OnFileDrop(List<string> filePaths);
    }
}