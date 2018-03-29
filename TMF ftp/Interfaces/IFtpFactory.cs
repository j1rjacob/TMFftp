namespace TMF_ftp.Interfaces
{
    public interface IFtpFactory
    {
        IFtp CreateFtp(string source, string destination, string host, string username, string password, int port);
    }
}
