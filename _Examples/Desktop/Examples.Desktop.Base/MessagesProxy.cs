using Examples.Desktop.Base.ViewModels;
using JToolbox.Threading;
using System;
using System.Threading.Tasks;

namespace Examples.Desktop.Base
{
    public class MessagesProxy : ProducerConsumer<string>
    {
        private MainViewModel mainViewModel;

        public MessagesProxy(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public override Task ExceptionOccured(string item, Exception exc)
        {
            return Task.CompletedTask;
        }

        public override Task HandleItem(string item)
        {
            if (item == null)
            {
                mainViewModel.Messages = string.Empty;
            }
            else
            {
                mainViewModel.Messages += item;
            }
            return Task.CompletedTask;
        }
    }
}