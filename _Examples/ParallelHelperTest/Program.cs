using JToolbox.Core.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ParallelHelperTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var data = Enumerable.Range(1, 5).ToList();
            var helper = new ParallelHelper<int, int>();

            using (var execTime = new ExecutionTime("Start with thread pool..."))
            {
                helper.RunWithPool(TestFunc, data);
            }
            ShowResults(helper);

            using (var execTime = new ExecutionTime("Start with threads..."))
            {
                helper.RunWithThreads(TestFunc, data);
            }
            ShowResults(helper);

            Console.ReadKey();
        }

        private static void ShowResults(ParallelHelper<int, int> parallelHelper)
        {
            foreach (var output in parallelHelper.Outputs)
            {
                Console.WriteLine("Output: " + output.Output);
            }
        }

        private static int TestFunc(int arg)
        {
            Console.WriteLine("Input: " + arg);
            Thread.Sleep(2000);
            return arg * 2;
        }
    }

    public class ExecutionTime : IDisposable
    {
        private readonly Stopwatch stopwatch;

        public ExecutionTime(string message)
        {
            stopwatch = Stopwatch.StartNew();
            Console.WriteLine(message);
        }

        public void Dispose()
        {
            Console.WriteLine($"Finished in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}