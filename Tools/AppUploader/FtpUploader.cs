using FluentFTP;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AppUploader
{
    public class FtpUploader
    {
        public async Task UploadAppFile(UploadData uploadData, IUploadProgressHandler uploadProgressHandler)
        {
            using (var ftp = new FtpClient(uploadData.Hostname, uploadData.Username, uploadData.Password))
            {
                await ftp.ConnectAsync();

                var appFileProgress = new Progress<FtpProgress>(p => uploadProgressHandler?.OnProgress("App uploading", p.Progress));
                await ftp.UploadFileAsync(uploadData.FilePath, uploadData.TargetFilePath, FtpRemoteExists.Overwrite, true, FtpVerify.None, appFileProgress);

                var updaterFile = string.Empty;
                try
                {
                    updaterFile = uploadData.CreateUpdaterFile();

                    var updaterFileProgress = new Progress<FtpProgress>(p => uploadProgressHandler?.OnProgress("Creating updater file", p.Progress));
                    await ftp.UploadFileAsync(updaterFile, uploadData.TargetFilePath, FtpRemoteExists.Overwrite, true, FtpVerify.None, updaterFileProgress);
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