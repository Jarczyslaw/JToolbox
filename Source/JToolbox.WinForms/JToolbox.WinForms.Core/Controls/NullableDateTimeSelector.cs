using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public partial class NullableDateTimeSelector : UserControl
    {
        public NullableDateTimeSelector()
        {
            InitializeComponent();

            UpdateDtsState();
            chb.CheckedChanged += Chb_CheckedChanged;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CheckBox CheckBox => chb;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTimeSelector DateTimeSelector => dts;

        public DateTime? Value
        {
            get
            {
                if (chb.Checked)
                {
                    return dts.Value;
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    chb.Checked = false;
                    dts.Value = DateTime.Now;
                }
                else
                {
                    chb.Checked = true;
                    dts.Value = value.Value;
                }
            }
        }

        private void Chb_CheckedChanged(object sender, System.EventArgs e)
        {
            UpdateDtsState();
        }

        private void UpdateDtsState()
        {
            dts.Enabled = chb.Checked;
        }
    }
}