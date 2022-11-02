using System;

namespace JToolbox.WinForms.Core.Controls
{
    [Serializable]
    public class ComboItem<T>
    {
        public string Header { get; set; }

        public T Value { get; set; }
    }
}