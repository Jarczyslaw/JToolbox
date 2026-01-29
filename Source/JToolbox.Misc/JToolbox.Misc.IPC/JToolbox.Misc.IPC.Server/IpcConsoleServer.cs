using StreamJsonRpc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JToolbox.Misc.IPC.Server
{
    public static class IpcConsoleServer
    {
        public static async Task Run(object contract)
        {
            Stream outputStream = Console.OpenStandardOutput();
            Stream inputStream = Console.OpenStandardInput();

            using (JsonRpc jsonRpc = JsonRpc.Attach(outputStream, inputStream, contract))
            {
                await jsonRpc.Completion;
            }
        }
    }
}