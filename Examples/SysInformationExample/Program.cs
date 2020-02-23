using JToolbox.Core.Extensions;
using JToolbox.SysInformation;
using System;

namespace SysInformationExample
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("OS information:");
            Console.WriteLine(SystemInformation.GetOSInfo().PublicPropertiesToString());
            Console.WriteLine();

            Console.WriteLine("CPU information:");
            Console.WriteLine(SystemInformation.GetCPUInfo().PublicPropertiesToString());
            Console.WriteLine();

            Console.WriteLine("Memory information:");
            Console.WriteLine(SystemInformation.GetMemoryInfo().PublicPropertiesToString());
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}