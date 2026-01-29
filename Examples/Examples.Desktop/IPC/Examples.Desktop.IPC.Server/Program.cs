using JToolbox.Misc.IPC.Server;

namespace Examples.Desktop.IPC.Server
{
    internal class Program
    {
        private static async Task Main(string[] _)
        {
            await IpcConsoleServer.Run(new Contract());
        }
    }
}