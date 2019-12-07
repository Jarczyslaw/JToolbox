using System.Collections.Generic;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public delegate void BufferedGridItemClick<T>(object sender, DataGridViewCellEventArgs args, T item);

    public delegate void BufferedGridItemDoubleClick<T>(object sender, DataGridViewCellEventArgs args, T item);

    public delegate void BufferedGridItemRightClick<T>(object sender, BufferedGridMouseEventArgs args, T item);

    public abstract class BufferedGrid<T> : DataGridView
    {
        public event BufferedGridItemClick<T> OnItemClick = delegate { };

        public event BufferedGridItemDoubleClick<T> OnItemDoubleClick = delegate { };

        public event BufferedGridItemRightClick<T> OnItemRightClick = delegate { };

        protected BufferedGrid()
        {
            Initialize();
            CellDoubleClick += BufferedGrid_CellDoubleClick;
            MouseClick += BufferedGrid_MouseClick;
            CellContentClick += BufferedGrid_CellContentClick;
        }

        public int? SelectedIndex
        {
            get
            {
                if (SelectedRows.Count > 0)
                {
                    return SelectedRows[0].Index;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ClearSelection();
                if (SelectedIndex.HasValue)
                {
                    Rows[SelectedIndex.Value].Selected = true;
                }
            }
        }

        public T SelectedItem
        {
            get
            {
                var selectedItems = SelectedItems;
                if (selectedItems.Count == 0)
                {
                    return default;
                }

                return selectedItems[0];
            }
        }

        public List<T> SelectedItems
        {
            get
            {
                var result = new List<T>();
                if (SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow selectedRow in SelectedRows)
                    {
                        result.Add((T)selectedRow.DataBoundItem);
                    }
                }
                return result;
            }
        }

        public List<T> Items
        {
            get => DataSource as List<T>;
            set
            {
                DataSource = null;
                DataSource = value;
                ClearSelection();
            }
        }

        private void Initialize()
        {
            DoubleBuffered = true;
            AutoGenerateColumns = false;
            AllowUserToAddRows =
                AllowUserToDeleteRows =
                AllowUserToOrderColumns =
                AllowUserToResizeColumns =
                AllowUserToResizeRows = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            ReadOnly = true;
        }

        private void BufferedGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var item = Rows[e.RowIndex].DataBoundItem;
                OnItemClick.Invoke(sender, e, (T)item);
            }
        }

        private void BufferedGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var col = e.ColumnIndex;
            if (row >= 0 && col >= 0)
            {
                OnItemDoubleClick.Invoke(sender, e, (T)Rows[row].DataBoundItem);
            }
        }

        private void BufferedGrid_MouseClick(object sender, MouseEventArgs e)
        {
            var hitTest = HitTest(e.X, e.Y);
            var args = new BufferedGridMouseEventArgs(e, hitTest.RowIndex, hitTest.ColumnIndex);
            if (args.RowIndex >= 0 && args.ColumnIndex >= 0)
            {
                var item = Rows[args.RowIndex].DataBoundItem;
                if (e.Button == MouseButtons.Right)
                {
                    OnItemRightClick.Invoke(sender, args, (T)item);
                }
            }
        }
    }
}