﻿using System.Collections.Generic;

namespace JToolbox.XamarinForms.Core.Abstraction
{
    public interface IAppCore
    {
        string DeviceId { get; }
        string LogPath { get; }
        void Kill();
        void FilesScan(List<string> files);
    }
}