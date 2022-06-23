using System.ComponentModel;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public partial class NullableNumericUpDown : UserControl
    {
        public NullableNumericUpDown()
        {
            InitializeComponent();
            UpdateNudState();
            chb.CheckedChanged += Chb_CheckedChanged;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CheckBox CheckBox => chb;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NumericUpDown NumericUpDown => nud;

        public decimal? Value
        {
            get
            {
                if (chb.Checked)
                {
                    return nud.Value;
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    chb.Checked = false;
                    nud.Value = nud.Minimum;
                }
                else
                {
                    chb.Checked = true;
                    nud.Value = value.Value;
                }
            }
        }

        private void Chb_CheckedChanged(object sender, System.EventArgs e)
        {
            UpdateNudState();
        }

        private void UpdateNudState()
        {
            nud.Enabled = chb.Checked;
        }
    }
}