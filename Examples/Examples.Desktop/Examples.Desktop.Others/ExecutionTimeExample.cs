using Examples.Desktop.Base;
using JToolbox.Core.Utilities;
using System;
using System.Threading.Tasks;

namespace Examples.Desktop.Others
{
    public class ExecutionTimeExample : BaseExample
    {
        public override async Task Run(IOutputInput outputInput)
        {
            outputInput.WriteLine("Start");

            ExecutionTime.Start();

            await Wait(1);
            ExecutionTime.Check("1 second");
            outputInput.WriteLine("1 second");

            await Wait(2);
            ExecutionTime.Check("2 seconds");
            outputInput.WriteLine("2 seconds");

            await Wait(3);
            ExecutionTime.Check("3 seconds");
            outputInput.WriteLine("3 seconds");

            outputInput.WriteLine(ExecutionTime.GetChecksResult());
        }

        private Task Wait(int seconds)
        {
            return Task.Delay(TimeSpan.FromSeconds(seconds));
        }
    }
}