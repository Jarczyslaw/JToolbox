using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public delegate void ValueChanged(DateTime from, DateTime to);

    public partial class DateRangeSelector : UserControl
    {
        public DateRangeSelector()
        {
            InitializeComponent();

            dtpFrom.DateTimeValueChanged += DtpFrom_DateTimeChanged;
            dtpTo.DateTimeValueChanged += DtpTo_DateTimeChanged;
        }

        public event ValueChanged OnValueChanged;

        public string CustomFormat
        {
            get
            {
                if (dtpFrom.CustomFormat != dtpTo.CustomFormat) { throw new Exception("Invalid custom formats"); }

                return dtpFrom.CustomFormat;
            }
            set
            {
                dtpFrom.CustomFormat
                    = dtpTo.CustomFormat = value;
            }
        }

        public DateTimePickerFormat Format
        {
            get
            {
                if (dtpFrom.Format != dtpTo.Format) { throw new Exception("Invalid date time formats"); }

                return dtpFrom.Format;
            }
            set
            {
                dtpFrom.Format
                    = dtpTo.Format = value;
            }
        }

        public DateTime From
        {
            get => dtpFrom.Value;
            set => SetFrom(value, false);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTimePicker FromDateTimePicker => dtpFrom;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Label FromLabel => lblFrom;

        public string FromLabelText
        {
            get => lblFrom.Text;
            set => lblFrom.Text = value;
        }

        public DateTime To
        {
            get => dtpTo.Value;
            set => SetTo(value, false);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTimePicker ToDateTimePicker => dtpTo;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Label ToLabel => lblTo;

        public string ToLabelText
        {
            get => lblTo.Text;
            set => lblTo.Text = value;
        }

        public void SetFrom(DateTime value, bool invokeEvents = false)
        {
            if (invokeEvents)
            {
                dtpFrom.Value = value;
            }
            else
            {
                dtpFrom.SetDateTimeValue(value, false);
                TrimFromValue();
            }
        }

        public void SetTo(DateTime value, bool invokeEvents = false)
        {
            if (invokeEvents)
            {
                dtpTo.Value = value;
            }
            else
            {
                dtpTo.SetDateTimeValue(value, false);
                TrimToValue();
            }
        }

        public void SetupDateTimePickers(Action<DateTimePicker> action)
        {
            action(dtpFrom);
            action(dtpTo);
        }

        private void DtpFrom_DateTimeChanged(object sender, EventArgs e)
        {
            TrimFromValue();
            InvokeOnValueChanged();
        }

        private void DtpTo_DateTimeChanged(object sender, EventArgs e)
        {
            TrimToValue();
            InvokeOnValueChanged();
        }

        private void InvokeOnValueChanged()
        {
            OnValueChanged?.Invoke(From, To);
        }

        private void TrimFromValue()
        {
            if (From > To)
            {
                dtpTo.SetDateTimeValue(From, false);
            }
        }

        private void TrimToValue()
        {
            if (To < From)
            {
                dtpFrom.SetDateTimeValue(To, false);
            }
        }
    }
}