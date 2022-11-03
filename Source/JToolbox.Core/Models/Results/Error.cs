using System;

namespace JToolbox.Core.Models.Results
{
    public class Error : Message
    {
        public Exception Exception { get; set; }

        public bool IsException => Exception != null;
    }
}