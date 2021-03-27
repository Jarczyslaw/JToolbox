using Examples.Desktop.Base;
using JToolbox.Threading;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Desktop.Threading
{
    public class ProcessingQueueExample : IDesktopExample
    {
        public string Title => "Processing queue";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public async Task Run(IOutputInput outputInput)
        {
            var queue = new Queue(outputInput);
            var numbers = Enumerable.Range(1, 5);
            var items = numbers.Select(s => new ProcessingQueueItem<int, int>(s)).ToList();

            queue.TasksCount = 1;
            outputInput.WriteLine($"Processing started with {queue.TasksCount} task");
            outputInput.StartTime();
            await queue.Run(items);
            outputInput.WriteLine($"Processed {items.Count(i => i.Processed)} items");
            outputInput.StopTime();

            queue.TasksCount = 4;
            outputInput.WriteLine($"Processing started with {queue.TasksCount} tasks");
            outputInput.StartTime();
            await queue.Run(items);
            outputInput.WriteLine($"Processed {items.Count(i => i.Processed)} items");
            outputInput.StopTime();

            queue.TasksCount = 1;
            var delay = 3000;
            var cts = new CancellationTokenSource(delay);
            outputInput.WriteLine($"Processing started with {queue.TasksCount} tasks. Will be cancelled in {delay}ms");
            outputInput.StartTime();
            await queue.Run(items, cts.Token);
            outputInput.WriteLine($"Processed {items.Count(i => i.Processed)} items");
            outputInput.StopTime();

            queue.TasksCount = 1;
            queue.StopOnFirstException = true;
            queue.Throw = true;
            outputInput.WriteLine($"Processing started with {queue.TasksCount} tasks. Will crash soon");
            outputInput.StartTime();
            await queue.Run(items);
            outputInput.WriteLine($"Processed {items.Count(i => i.Processed)} items");
            outputInput.StopTime();
        }

        private class Queue : ProcessingQueue<int, int>
        {
            private readonly IOutputInput outputInput;
            public bool Throw { get; set; }

            public Queue(IOutputInput outputInput)
            {
                this.outputInput = outputInput;
            }

            public override async Task<int> ProcessItem(int item)
            {
                if (!Throw || (Throw && item < 3))
                {
                    await Task.Delay(2000);
                    return item * 2;
                }
                throw new System.Exception("Expected exception");
            }

            public override Task ReportProgress(ProcessingQueueItem<int, int> item)
            {
                outputInput.WriteLine($"Processed item {item.Item} with result {item.Result}");
                return Task.CompletedTask;
            }
        }
    }
}