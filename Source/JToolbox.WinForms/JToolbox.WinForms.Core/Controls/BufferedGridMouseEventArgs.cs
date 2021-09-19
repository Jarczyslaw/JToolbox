using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public class BufferedGridMouseEventArgs
    {
        public BufferedGridMouseEventArgs(MouseEventArgs mouseEventArgs, int rowIndex, int columnIndex)
        {
            MouseEventArgs = mouseEventArgs;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        public int ColumnIndex { get; }
        public MouseEventArgs MouseEventArgs { get; }
        public int RowIndex { get; }
    }
}