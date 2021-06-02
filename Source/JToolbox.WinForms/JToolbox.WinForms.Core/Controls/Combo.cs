using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public delegate void OnComboItemChanged<T>(T value);

    public class Combo<T> : ComboBox
    {
        public event OnComboItemChanged<T> OnComboItemChanged = delegate { };

        public Combo()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            DisplayMember = nameof(ComboItem<T>.Header);
            ValueMember = nameof(ComboItem<T>.Value);
            SelectedValueChanged += Combo_SelectedValueChanged;
        }

        public List<ComboItem<T>> ComboItems
        {
            get => DataSource as List<ComboItem<T>>;
            set
            {
                DataSource = null;
                DataSource = value;
                if (value != null)
                {
                    SelectedValue = value.FirstOrDefault();
                }
            }
        }

        public T SelectedComboItem
        {
            get => (T)SelectedValue;
            set => SelectedValue = value;
        }

        private void Combo_SelectedValueChanged(object sender, EventArgs e)
        {
            OnComboItemChanged?.Invoke(SelectedComboItem);
        }
    }
}