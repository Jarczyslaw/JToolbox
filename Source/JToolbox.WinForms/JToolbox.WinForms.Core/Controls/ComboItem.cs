using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JToolbox.WinForms.Core.Controls
{
    public class ComboItem<T>
    {
        public string Header { get; set; }
        public T Value { get; set; }
    }
}
