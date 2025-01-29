using Examples.Desktop.Base;
using JToolbox.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.Desktop.Others
{
    public class TimeBufferExample : BaseExample
    {
        private IntTimeBuffer _timeBuffer;
        private int counter;

        public override string CustomActionTitle => "Add item";

        public override void CustomAction()
        {
            if (_timeBuffer != null)
            {
                counter++;
                _timeBuffer.Add(counter);
            }
        }

        public override async Task Run(IOutputInput outputInput)
        {
            _timeBuffer = new IntTimeBuffer(outputInput);

            await outputInput.Wait();
        }

        private class IntTimeBuffer : TimeBuffer<int>
        {
            private readonly IOutputInput _outputInput;

            public IntTimeBuffer(IOutputInput outputInput)
            {
                _outputInput = outputInput;
                MaxItemsCount = 3;
                Timeout = TimeSpan.FromSeconds(3);
            }

            protected override void Flush(List<int> itemsToFlush)
            {
                _outputInput.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Flushed items: {string.Join(", ", itemsToFlush)}");
            }
        }
    }
}