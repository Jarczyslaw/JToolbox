using System.Collections.Generic;

namespace JToolbox.WPF.Core.Awareness
{
    public interface IFileDragDropAware
    {
        List<string> OnFileDrag();

        void OnFilesDrop(List<string> filesPaths);
    }
}