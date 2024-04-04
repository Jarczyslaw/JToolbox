using Examples.Desktop.Base;
using JToolbox.Core.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Desktop.Others
{
    public class ExecutionTimeExample : BaseExample
    {
        public override async Task Run(IOutputInput outputInput)
        {
            outputInput.WriteLine("Start");

            var executionTime = new ExecutionTime();

            await Wait(1);
            executionTime.Check("1 second");
            outputInput.WriteLine("1 second");

            await Wait(2);
            executionTime.Check("2 seconds");
            outputInput.WriteLine("2 seconds");

            await Wait(3);
            executionTime.Check("3 seconds");
            outputInput.WriteLine("3 seconds");

            outputInput.WriteLine(executionTime.GetChecksResult());

            executionTime.Start();

            await Wait(1);
            executionTime.Check("1 second", false);
            outputInput.WriteLine("1 second");

            await Wait(1);

            executionTime.Start();

            await Wait(2);
            executionTime.Check("2 seconds", false);
            outputInput.WriteLine("2 seconds");

            outputInput.WriteLine(executionTime.GetChecksResult());

            ExecutionTime.RunAction(() => Thread.Sleep(1000), "RunAction");

            int result = ExecutionTime.RunFunc(() =>
            {
                Thread.Sleep(1000);
                return 0;
            }, "RunFunc");

            await ExecutionTime.RunActionAsync(async () => await Wait(1), "RunActionAsync");

            result = await ExecutionTime.RunFuncAsync(async () =>
            {
                await Wait(1);
                return 0;
            }, "RunFuncAsync");
        }

        private Task Wait(int seconds)
        {
            return Task.Delay(TimeSpan.FromSeconds(seconds));
        }
    }
}