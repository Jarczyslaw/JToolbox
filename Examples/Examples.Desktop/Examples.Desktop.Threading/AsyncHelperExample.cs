using Examples.Desktop.Base;
using JToolbox.Misc.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.Desktop.Threading
{
    public class AsyncHelperExample : IDesktopExample
    {
        public string Title => "AsyncHelper";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public async Task Run(IOutputInput outputInput)
        {
            var items = Enumerable.Range(1, 5)
                .ToList();

            outputInput.WriteLine("5 tasks for 5 items without result which should finish in circa 2 seconds:");
            await AsyncHelper.ForEach(items, async (item, cancellationToken) =>
            {
                await Task.Delay(2000);
                outputInput.WriteLine($"Result: {item * 2}");
            });

            outputInput.PutLine();
            outputInput.WriteLine("5 tasks for 5 items without result which should finish in circa 2 seconds:");
            var result  = await AsyncHelper.ForEachWithResult(items, async (item, cancellationToken) =>
            {
                await Task.Delay(2000);
                return item * 2;
            });
            foreach (var pair in result)
            {
                outputInput.WriteLine($"Item: {pair.Key}, result: {pair.Value}");
            }
        }
    }
}