using JToolbox.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public delegate void BufferedGridItemClick<T>(object sender, DataGridViewCellEventArgs args, T item);

    public delegate void BufferedGridItemDoubleClick<T>(object sender, DataGridViewCellEventArgs args, T item);

    public delegate void BufferedGridItemRightClick<T>(object sender, BufferedGridMouseEventArgs args, T item);

    public abstract class BufferedGrid<T> : DataGridView
        where T : class
    {
        public event BufferedGridItemClick<T> OnItemClick = delegate { };

        public event BufferedGridItemDoubleClick<T> OnItemDoubleClick = delegate { };

        public event BufferedGridItemRightClick<T> OnItemRightClick = delegate { };

        private readonly BindingSource bindingSource = new BindingSource();

        protected BufferedGrid()
        {
            DataSource = bindingSource;
            CellDoubleClick += BufferedGrid_CellDoubleClick;
            MouseClick += BufferedGrid_MouseClick;
            CellContentClick += BufferedGrid_CellContentClick;
        }

        public bool ItemsSelected => SelectedRows.Count > 0;

        public int? SelectedIndex
        {
            get
            {
                if (ItemsSelected)
                {
                    return bindingSource.Position;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    bindingSource.Position = value.Value;
                }
                else
                {
                    ClearSelection();
                }
            }
        }

        public T SelectedItem
        {
            get
            {
                if (ItemsSelected)
                {
                    return bindingSource.Current as T;
                }
                return null;
            }
        }

        public List<T> SelectedItems
        {
            get
            {
                var result = new List<T>();
                if (ItemsSelected)
                {
                    foreach (DataGridViewRow row in SelectedRows)
                    {
                        result.Add(row.DataBoundItem as T);
                    }
                }
                return result;
            }
        }

        public List<T> Items
        {
            get => bindingSource.DataSource as List<T>;
            set
            {
                bindingSource.DataSource = null;
                bindingSource.DataSource = value;
                ClearSelection();
            }
        }

        public void UpdateItem(T item, Predicate<T> predicate)
        {
            if (Items != null)
            {
                Items.Replace(item, predicate);
                UpdateBinding();
            }
        }

        public void UpdateBinding()
        {
            bindingSource.ResetBindings(false);
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