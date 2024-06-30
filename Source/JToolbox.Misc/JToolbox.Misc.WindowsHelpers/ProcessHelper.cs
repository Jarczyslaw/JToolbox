using System.Diagnostics;

namespace JToolbox.Misc.WindowsHelpers
{
    public static class ProcessHelper
    {
        public static Process RunCommand(string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = command
            };
            process.StartInfo.UseShellExecute = false;
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();

            return process;
        }
    }
}