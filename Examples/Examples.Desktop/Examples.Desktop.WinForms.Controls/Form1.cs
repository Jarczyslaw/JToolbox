using JToolbox.WinForms.Core.Controls;
using System;
using System.Windows.Forms;

namespace Examples.Desktop.WinForms.Controls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dateRangeSelector1.OnValueChanged += DateRangeSelector1_OnValueChanged;
            dateTimeSelector1.DateTimeValueChanged += DateTimeSelector1_DateTimeChanged;
        }

        private void AppendLog(string message)
        {
            textBox1.Text += message + Environment.NewLine;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateRangeSelector1.From += TimeSpan.FromDays(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateRangeSelector1.To += TimeSpan.FromDays(-1);
        }

        private void DateRangeSelector1_OnValueChanged(DateTime from, DateTime to)
        {
            AppendLog($"DateRangeSelector changed [from: {from:yyyy/MM/dd}, to: {to:yyyy/MM/dd}]");
        }

        private void DateTimeSelector1_DateTimeChanged(object sender, EventArgs e)
        {
            AppendLog($"dateTimeSelector1 changed {dateTimeSelector1.Value:yyyy/MM/dd}]");
        }
    }
}