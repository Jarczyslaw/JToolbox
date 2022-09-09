using FluentFTP;
using System;
using System.IO;

namespace AppUploader
{
    public class FtpUploader
    {
        public bool UploadAppFile(UploadData uploadData, IUploadProgressHandler uploadProgressHandler)
        {
            using (var ftp = new FtpClient(uploadData.Hostname, uploadData.Username, uploadData.Password))
            {
                ftp.Connect();

                var appFileProgress = new Action<FtpProgress>(p => uploadProgressHandler?.OnProgress("App uploading", p.Progress));
                var uploadResult = ftp.UploadFile(uploadData.FilePath, uploadData.TargetFilePath, FtpRemoteExists.Overwrite, true, FtpVerify.None, appFileProgress);
                if (uploadResult != FtpStatus.Success) { return false; }

                var updaterFile = string.Empty;
                try
                {
                    updaterFile = uploadData.CreateUpdaterFile();

                    var updaterFileProgress = new Action<FtpProgress>(p => uploadProgressHandler?.OnProgress("Creating updater file", p.Progress));
                    uploadResult = ftp.UploadFile(updaterFile, uploadData.UpdaterFilePath, FtpRemoteExists.Overwrite, true, FtpVerify.None, updaterFileProgress);
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