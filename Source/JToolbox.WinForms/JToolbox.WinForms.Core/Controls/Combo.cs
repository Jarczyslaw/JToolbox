using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public delegate void ComboValueChanged<T>(T value);

    public class Combo<T> : ComboBox
    {
        public Combo()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            SelectionChangeCommitted += Combo_SelectionChangeCommitted;
        }

        public event ComboValueChanged<T> OnComboValueChanged = delegate { };

        public List<ComboItem<T>> ComboItems
        {
            get => DataSource as List<ComboItem<T>>;
            set
            {
                var selectedValue = SelectedComboValue;

                DataSource = null;
                DataSource = value;
                DisplayMember = nameof(ComboItem<T>.Header);
                ValueMember = nameof(ComboItem<T>.Value);

                SelectedComboValue = selectedValue;
            }
        }

        public ComboItem<T> SelectedComboItem
        {
            get
            {
                if (SelectedItem != null)
                {
                    return SelectedItem as ComboItem<T>;
                }
                return null;
            }
            set => SelectedItem = value;
        }

        public T SelectedComboValue
        {
            get
            {
                if (SelectedComboItem != null)
                {
                    return SelectedComboItem.Value;
                }
                return default;
            }
            set
            {
                if (value != null)
                {
                    SelectedComboItem = ComboItems?.Find(i => EqualityComparer<T>.Default.Equals(i.Value, value));
                }
                else
                {
                    SelectedComboItem = null;
                }
            }
        }

        private void Combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnComboValueChanged?.Invoke(SelectedComboValue);
        }
    }
}