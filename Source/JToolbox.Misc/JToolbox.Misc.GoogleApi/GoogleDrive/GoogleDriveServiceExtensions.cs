using Google;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Upload;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JToolbox.Misc.GoogleApi.GoogleDrive
{
    public static class GoogleDriveServiceExtensions
    {
        private const string CSV_CONTENT_TYPE = "text/csv";
        private const string SHEETS_MIME_TYPE = "application/vnd.google-apps.spreadsheet";

        public static async Task<(IUploadProgress, string)> CreateCsvFile(
            this DriveService service,
            string filePath,
            string fileName = null,
            IList<string> parents = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(filePath);
            }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                MimeType = SHEETS_MIME_TYPE,
                Parents = parents
            };

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return await CreateFile(service, fileMetadata, fileStream, CSV_CONTENT_TYPE);
            }
        }

        public static async Task<(IUploadProgress, string)> CreateFile(
            this DriveService service,
            Google.Apis.Drive.v3.Data.File fileMetadata,
            Stream fileStream,
            string contentType)
        {
            var request = service.Files.Create(fileMetadata, fileStream, contentType);
            request.Fields = "id";

            var result = await request.UploadAsync();

            return (result, request.ResponseBody?.Id);
        }

        public static Task<string> DeleteFile(this DriveService service, string fileId)
        {
            var request = service.Files.Delete(fileId);
            return request.ExecuteAsync();
        }

        public static void DeleteFiles(this DriveService service, IEnumerable<string> fileIds)
        {
            foreach (var fileId in fileIds)
            {
                DeleteFile(service, fileId);
            }
        }

        public static async Task<Google.Apis.Drive.v3.Data.File> GetFile(this DriveService service, string fileId)
        {
            var request = service.Files.Get(fileId);

            try
            {
                return await request.ExecuteAsync();
            }
            catch (GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public static Task<FileList> GetFiles(this DriveService service, string query = default)
        {
            var request = service.Files.List();
            request.Q = query;
            return request.ExecuteAsync();
        }

        public static Task<FileList> GetFilesByName(this DriveService service, string fileName)
            => service.GetFiles($"name = '{fileName}'");

        public static Task<Permission> ShareFile(this DriveService service, string fileId, Permission permission)
        {
            var request = service.Permissions.Create(permission, fileId);
            request.SendNotificationEmail = false;
            return request.ExecuteAsync();
        }

        public static Task<Permission> ShareFileForReader(this DriveService service, string fileId, string emailAddress)
        {
            var permission = new Permission
            {
                Type = "user",
                EmailAddress = emailAddress,
                Role = "reader"
            };

            return service.ShareFile(fileId, permission);
        }

        public static Task<Permission> ShareFileForWriter(this DriveService service, string fileId, string emailAddress)
        {
            var permission = new Permission
            {
                Type = "user",
                EmailAddress = emailAddress,
                Role = "writer"
            };

            return service.ShareFile(fileId, permission);
        }

        public static async Task<(IUploadProgress, bool)> UpdateCsvFile(
            this DriveService service,
            string filePath,
            string fileId = null)
        {
            Google.Apis.Drive.v3.Data.File file = null;
            if (string.IsNullOrEmpty(fileId))
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var files = await GetFilesByName(service, fileName);

                if (files.Files.Count == 1)
                {
                    file = files.Files[0];
                }
            }
            else
            {
                file = await service.GetFile(fileId);
            }

            if (file == null) { return (null, false); }

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return await UpdateFile(service, new Google.Apis.Drive.v3.Data.File(), file.Id, fileStream, CSV_CONTENT_TYPE);
            }
        }

        public static async Task<(IUploadProgress, bool)> UpdateFile(
            this DriveService service,
            Google.Apis.Drive.v3.Data.File fileMetadata,
            string fileId,
            Stream fileStream,
            string contentType)
        {
            var request = service.Files.Update(fileMetadata, fileId, fileStream, contentType);
            var result = await request.UploadAsync();

            return (result, result.Status == UploadStatus.Completed);
        }
    }
}