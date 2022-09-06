using System;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Controls
{
    public class DateTimeSelector : DateTimePicker
    {
        private DateTime tempDate;

        public DateTimeSelector()
        {
            CloseUp += DateTimeSelector_CloseUp;
            DropDown += DateTimeSelector_DropDown;

            ValueChanged += DateTimeSelector_ValueChanged;
        }

        public event EventHandler DateTimeValueChanged;

        public void SetDateTimeValue(DateTime value, bool invokeEvents)
        {
            if (invokeEvents)
            {
                Value = value;
            }
            else
            {
                ValueChanged -= DateTimeSelector_ValueChanged;
                Value = value;
                ValueChanged += DateTimeSelector_ValueChanged;
            }
        }

        private void DateTimeSelector_CloseUp(object sender, EventArgs e)
        {
            ValueChanged += DateTimeSelector_ValueChanged;
            if (Value != tempDate)
            {
                DateTimeSelector_ValueChanged(sender, e);
            }
        }

        private void DateTimeSelector_DropDown(object sender, EventArgs e)
        {
            tempDate = Value;
            ValueChanged -= DateTimeSelector_ValueChanged;
        }

        private void DateTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            DateTimeValueChanged?.Invoke(sender, e);
        }
    }
}