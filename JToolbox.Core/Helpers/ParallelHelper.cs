using System;
using System.Collections.Generic;
using System.Threading;

namespace JToolbox.Core.Helpers
{
    public class ParallelHelper<TInput, TOutput>
    {
        private static readonly object listLock = new object();
        private readonly List<ParallelHelperOutput> outputs = new List<ParallelHelperOutput>();
        private Exception runException;

        public List<ParallelHelperOutput> Outputs
        {
            get
            {
                lock (listLock)
                {
                    return outputs;
                }
            }
        }

        public Exception Exception
        {
            get
            {
                if (runException != null)
                {
                    return runException;
                }

                var exc = Outputs.Find(o => o.Exception != null);
                if (exc != null)
                {
                    return exc.Exception;
                }
                return null;
            }
        }

        private void Run(Action runInBackgroundAction)
        {
            try
            {
                Clear();
                runInBackgroundAction();
            }
            catch (Exception exc)
            {
                runException = exc;
            }
        }

        public void RunInBackground(Func<TInput, TOutput> func, List<TInput> input)
        {
            ThreadPool.GetMaxThreads(out int workersThreads, out int _);
            if (input.Count > workersThreads)
            {
                RunWithThreads(func, input);
            }
            else
            {
                RunWithPool(func, input);
            }
        }

        public void RunWithThreads(Func<TInput, TOutput> func, List<TInput> input)
        {
            Run(() =>
            {
                var threads = new List<Thread>();
                for (int i = 0; i < input.Count; i++)
                {
                    var thread = new Thread(state => CallWrappedFunc(func, state));
                    threads.Add(thread);
                    thread.Start(input[i]);
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }
            });
        }

        public void RunWithPool(Func<TInput, TOutput> func, List<TInput> input)
        {
            Run(() =>
            {
                var threads = input.Count;
                using (var resetEvent = new ManualResetEvent(false))
                {
                    for (int i = 0; i < threads; i++)
                    {
                        ThreadPool.QueueUserWorkItem(state =>
                        {
                            CallWrappedFunc(func, state);
                            if (Interlocked.Decrement(ref threads) == 0)
                            {
                                resetEvent.Set();
                            }
                        }, input[i]);
                    }
                    resetEvent.WaitOne();
                }
            });
        }

        private void CallWrappedFunc(Func<TInput, TOutput> func, object state)
        {
            var managerOutput = new ParallelHelperOutput()
            {
                Input = (TInput)state
            };
            try
            {
                managerOutput.Output = func(managerOutput.Input);
            }
            catch (Exception exc)
            {
                managerOutput.Output = default(TOutput);
                managerOutput.Exception = exc;
            }
            Outputs.Add(managerOutput);
        }

        public void Clear()
        {
            Outputs.Clear();
            runException = null;
        }

        public class ParallelHelperOutput
        {
            public TInput Input { get; set; }
            public TOutput Output { get; set; }
            public Exception Exception { get; set; }
        }
    }
}