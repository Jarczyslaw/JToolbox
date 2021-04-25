using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public class BufferedGridMouseEventArgs
    {
        public MouseEventArgs MouseEventArgs { get; }
        public int RowIndex { get; }
        public int ColumnIndex { get; }

        public BufferedGridMouseEventArgs(MouseEventArgs mouseEventArgs, int rowIndex, int columnIndex)
        {
            MouseEventArgs = mouseEventArgs;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }
    }
}
