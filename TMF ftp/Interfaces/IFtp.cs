namespace TMF_ftp.Interfaces
{
    public interface IFtp
    {
        void Connect();
        bool DirIsEmpty(string source);
        void DownloadDir();
        void DownloadFile();
        void DownloadTask();
        void DeleteTask();
    }
}
