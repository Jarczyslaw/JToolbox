﻿using Examples.Desktop.Base;
using JToolbox.Core.Extensions;
using JToolbox.Misc.SysInformation;
using System.Threading.Tasks;

namespace Examples.Desktop.SysInformation
{
    public class MemoryInformation : IDesktopExample
    {
        public string CustomActionTitle => null;

        public string Title => "Memory Information";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public void CustomAction()
        {
            throw new System.NotImplementedException();
        }

        public Task Run(IOutputInput outputInput)
        {
            outputInput.WriteLine("Memory information:");
            outputInput.WriteLine(SystemInformation.GetMemoryInfo().PublicPropertiesToString());
            return Task.CompletedTask;
        }
    }
}