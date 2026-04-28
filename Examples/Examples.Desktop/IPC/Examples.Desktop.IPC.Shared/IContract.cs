using Examples.Desktop.IPC.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.Desktop.IPC.Shared
{
    public interface IContract
    {
        Task ExceptionTest();

        Task<List<Item>> ProcessItems(List<Item> items);
    }
}