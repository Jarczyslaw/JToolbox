using JToolbox.Misc.TimeProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Examples.Desktop.TimeProviders
{
    internal class Program
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
                    var tasks = new List<Task>();
                    foreach (var provider in providers)
                    {
                        tasks.Add(new Task(() =>
                        {
                            var stopwatch = Stopwatch.StartNew();
                            var now = provider.Now();
                            var localNow = DateTime.Now;
                            var elapsed = stopwatch.Elapsed;
                            var diff = Math.Abs((localNow - now).TotalMilliseconds);
                            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: Now from {provider.GetType().Name}: \t{now:HH:mm:ss.fff}, elapsed: {elapsed.TotalMilliseconds}ms, diff: {diff}ms");
                        }));
                    }

                    tasks.ForEach(x => x.Start());
                    Task.WaitAll(tasks.ToArray());

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