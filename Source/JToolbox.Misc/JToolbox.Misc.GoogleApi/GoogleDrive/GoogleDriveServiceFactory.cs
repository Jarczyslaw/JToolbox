using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Misc.GoogleApi.GoogleDrive
{
    public static class GoogleDriveServiceFactory
    {
        private const string _driveScope = DriveService.ScopeConstants.Drive;

        public static async Task<DriveService> GetServiceWithOAuth(
            OAuthAuthorizationData data,
            CancellationToken cancellationToken = default)
        {
            var clientSecrets = new ClientSecrets
            {
                ClientId = data.ClientId,
                ClientSecret = data.ClientSecret
            };

            var authorizedUserCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                new List<string>() { _driveScope },
                data.UserName,
                cancellationToken,
                new FileDataStore(data.TokenPath, true));

            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = authorizedUserCredential,
                ApplicationName = data.ApplicationName
            });
        }

        public static DriveService GetServiceWithServiceAccount(string credentialsFilePath, string appName)
        {
            var credential = GoogleCredential.FromFile(credentialsFilePath)
                .CreateScoped(_driveScope);

            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = appName
            });
        }
    }
}