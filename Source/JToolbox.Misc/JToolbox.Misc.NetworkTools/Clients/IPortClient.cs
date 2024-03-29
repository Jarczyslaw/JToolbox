﻿using System.Net;
using System.Threading.Tasks;

namespace JToolbox.Misc.NetworkTools.Clients
{
    public interface IPortClient
    {
        Task<bool> Check(IPAddress address, int port, int timeout);
    }
}