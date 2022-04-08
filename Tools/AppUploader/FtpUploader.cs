using FluentFTP;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AppUploader
{
    public class FtpUploader
    {
        public async Task<bool> UploadAppFile(UploadData uploadData, IUploadProgressHandler uploadProgressHandler)
        {
            using (var ftp = new FtpClient(uploadData.Hostname, uploadData.Username, uploadData.Password))
            {
                await ftp.ConnectAsync();

                var appFileProgress = new Progress<FtpProgress>(p => uploadProgressHandler?.OnProgress("App uploading", p.Progress));
                var uploadResult = await ftp.UploadFileAsync(uploadData.FilePath, uploadData.TargetFilePath, FtpRemoteExists.Overwrite, true, FtpVerify.None, appFileProgress);
                if (uploadResult != FtpStatus.Success) { return false; }

                var updaterFile = string.Empty;
                try
                {
                    updaterFile = uploadData.CreateUpdaterFile();

                    var updaterFileProgress = new Progress<FtpProgress>(p => uploadProgressHandler?.OnProgress("Creating updater file", p.Progress));
                    uploadResult = await ftp.UploadFileAsync(updaterFile, uploadData.UpdaterFilePath, FtpRemoteExists.Overwrite, true, FtpVerify.None, updaterFileProgress);
                    return uploadResult == FtpStatus.Success;
                }
                finally
                {
                    if (File.Exists(updaterFile))
                    {
                        File.Delete(updaterFile);
                    }
                }
            }
        }
    }
}