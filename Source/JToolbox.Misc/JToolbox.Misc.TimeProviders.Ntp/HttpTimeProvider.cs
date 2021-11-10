using System;
using System.Net;

namespace JToolbox.Misc.TimeProviders.Ntp
{
    public class HttpTimeProvider : TimeProviderBase
    {
        private readonly string uri;

        public HttpTimeProvider(string uri)
        {
            this.uri = uri;
        }

        protected override DateTime Synchronize()
        {
            var req = WebRequest.Create(uri);
            using (var resp = req.GetResponse())
            {
                var currTime = resp.Headers["date"];
                return ParseDateTimeOffset(currTime, "ddd, dd MMM yyyy HH:mm:ss 'GMT'");
            }
        }
    }
}