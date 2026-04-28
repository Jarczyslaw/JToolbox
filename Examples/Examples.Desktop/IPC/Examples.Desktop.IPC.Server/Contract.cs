using Examples.Desktop.IPC.Shared;
using Examples.Desktop.IPC.Shared.Models;

namespace Examples.Desktop.IPC.Server
{
    public class Contract : IContract
    {
        public Task ExceptionTest() => throw new Exception("Test exception");

        public Task<List<Item>> ProcessItems(List<Item> items)
        {
            if (items == null) { return Task.FromResult(items); }

            foreach (Item item in items)
            {
                item.Output = item.Input * 2;
            }

            return Task.FromResult(items);
        }
    }
}