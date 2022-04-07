namespace AppUploader
{
    public interface IUploadProgressHandler
    {
        void OnProgress(string taskName, double progress);
    }
}