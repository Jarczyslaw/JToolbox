using System.Collections.Generic;
using System.Drawing;
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
                    return default(T);
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

        public virtual void Initialize()
        {
            Columns.Clear();
            DoubleBuffered = true;
            AutoGenerateColumns = false;
            AllowUserToAddRows =
                AllowUserToDeleteRows =
                AllowUserToOrderColumns =
                AllowUserToResizeColumns =
                AllowUserToResizeRows = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CellBorderStyle = DataGridViewCellBorderStyle.Single;
            RowHeadersVisible = false;
            BorderStyle = BorderStyle.None;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            EnableHeadersVisualStyles = false;
            ShowCellToolTips = true;
            TabStop = false;
            MultiSelect = false;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = ColumnHeadersDefaultCellStyle.BackColor;
        }

        private DataGridViewTextBoxColumn GetTextColumn(string header, string mapping, int width)
        {
            var dgvc = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DataPropertyName = mapping,
                HeaderText = header,
                ReadOnly = true
            };

            if (width != 0)
            {
                dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvc.Width = width;
            }
            else
            {
                dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            return dgvc;
        }

        public void AddTextColumn(string header, string mapping, int width = 0)
        {
            var column = GetTextColumn(header, mapping, width);
            Columns.Add(column);
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