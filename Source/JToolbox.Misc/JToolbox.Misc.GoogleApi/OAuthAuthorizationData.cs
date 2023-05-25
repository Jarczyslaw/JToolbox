namespace JToolbox.Misc.GoogleApi
{
    public class OAuthAuthorizationData
    {
        public string ApplicationName { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TokenPath { get; set; }

        public string UserName { get; set; } = "user";
    }
}