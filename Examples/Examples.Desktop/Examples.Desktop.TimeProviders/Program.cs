using JToolbox.Misc.TimeProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Examples.Desktop.TimeProviders
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var providers = new List<ITimeProvider>
                {
                    new LocalTimeProvider(),
                    new NtpUdpTimeProvider("pool.ntp.org"),
                    new NtpTcpTimeProvider("time.nist.gov"),
                    new HttpTimeProvider("https://nist.time.gov")
                };

                while (true)
                {
                    foreach (var provider in providers)
                    {
                        var stopwatch = Stopwatch.StartNew();
                        var now = provider.Now();
                        var localNow = DateTime.Now;
                        var elapsed = stopwatch.Elapsed;
                        var diff = (now - localNow).TotalMilliseconds;
                        Console.WriteLine($"{localNow:HH:mm:ss.fff}: {provider.GetType().Name}: \t{now:HH:mm:ss.fff}, elapsed: {elapsed.TotalMilliseconds}ms, diff: {diff}ms");
                    }

                    Console.ReadKey();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            Console.ReadKey();
        }
    }
}