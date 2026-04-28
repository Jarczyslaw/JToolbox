using StreamJsonRpc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JToolbox.Misc.IPC.Client
{
    public static class IpcConsoleClient
    {
        public static async Task Run<TContract>(
            string processPath,
            string arguments,
            Func<TContract, Task> contractAction)
            where TContract : class
        {
            ProcessStartInfo startInfo = CreateStartInfo(processPath, arguments);

            using (Process process = Process.Start(startInfo))
            {
                try
                {
                    using (JsonRpc rpcConnection = new JsonRpc(process.StandardInput.BaseStream, process.StandardOutput.BaseStream))
                    {
                        TContract contract = rpcConnection.Attach<TContract>();
                        rpcConnection.StartListening();

                        await contractAction(contract);
                    }
                }
                finally
                {
                    if (!process.WaitForExit(1000))
                    {
                        process.Kill();
                    }
                }
            }
        }

        private static ProcessStartInfo CreateStartInfo(
            string processPath,
            string arguments)
        {
            return new ProcessStartInfo
            {
                FileName = processPath,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                Arguments = arguments
            };
        }
    }
}