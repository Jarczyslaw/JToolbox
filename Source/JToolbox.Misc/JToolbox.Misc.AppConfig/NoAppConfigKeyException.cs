using System;

namespace JToolbox.Misc.AppConfig
{
    public class NoAppConfigKeyException : Exception
    {
        public NoAppConfigKeyException() : base()
        {
        }

        public NoAppConfigKeyException(string message) : base(message)
        {
        }

        public NoAppConfigKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}