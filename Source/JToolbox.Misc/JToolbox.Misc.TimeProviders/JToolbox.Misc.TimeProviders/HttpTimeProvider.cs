using System;
using System.Globalization;
using System.Net;

namespace JToolbox.Misc.TimeProviders
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
            var resp = req.GetResponse();
            var currTime = resp.Headers["date"];
            var dt = DateTimeOffset.ParseExact(currTime,
                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AssumeUniversal);
            return dt.UtcDateTime.ToLocalTime();
        }
    }
}