using System.Windows.Forms;

namespace JToolbox.WinForms.Core.MessageBoxExt
{
    public static class MessageBoxUtils
    {
        public static void Error(string caption, string error)
        {
            MessageBox.Show(error, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Information(string caption, string information)
        {
            MessageBox.Show(information, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string OpenFile(string title, string filter)
        {
            var ofd = new OpenFileDialog
            {
                Title = title,
                Filter = filter,
                RestoreDirectory = true,
                Multiselect = false,
            };
            var dr = ofd.ShowDialog();
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
            {
                return ofd.FileName;
            }
            return null;
        }

        public static string SaveFile(string title, string filter, string fileName)
        {
            var sfd = new SaveFileDialog
            {
                Title = title,
                Filter = filter,
                RestoreDirectory = true,
                FileName = fileName
            };
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
            {
                return sfd.FileName;
            }

            return null;
        }

        public static DialogResult YesNoCancelQuestion(string caption, string question)
        {
            return MessageBox.Show(question, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        public static DialogResult YesNoCancelQuestion(string caption, string question, string yesText, string noText, string cancelText)
        {
            MessageBoxManager.Yes = yesText;
            MessageBoxManager.No = noText;
            MessageBoxManager.Cancel = cancelText;
            MessageBoxManager.Register();
            var result = YesNoCancelQuestion(caption, question);
            MessageBoxManager.Unregister();
            return result;
        }

        public static DialogResult YesNoQuestion(string caption, string question)
        {
            return MessageBox.Show(question, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult YesNoWarning(string caption, string question)
        {
            return MessageBox.Show(question, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}