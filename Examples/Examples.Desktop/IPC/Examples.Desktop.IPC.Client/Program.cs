using Examples.Desktop.IPC.Shared;
using Examples.Desktop.IPC.Shared.Models;
using JToolbox.Misc.IPC.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Examples.Desktop.IPC.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            string path = Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory,
                    @"..\..\..",
                    @"Examples.Desktop.IPC.Server\bin\Debug\net8.0\Examples.Desktop.IPC.Server.exe"));

            await IpcConsoleClient.Run<IContract>(
                path,
                async contract =>
                {
                    List<Item> items = new List<Item>
                    {
                        new Item
                        {
                            Input = 1
                        },
                        new Item
                        {
                            Input = 2
                        },
                        new Item
                        {
                            Input = 3
                        },
                    };

                    List<Item> result = await contract.ProcessItems(items);

                    Console.WriteLine("Result:");

                    foreach (Item item in result)
                    {
                        Console.WriteLine($"Input: {item.Input}, output: {item.Output}");
                    }
                });

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}