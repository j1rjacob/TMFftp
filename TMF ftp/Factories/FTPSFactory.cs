using TMF_ftp.Interfaces;
using TMF_ftp.Services;

namespace TMF_ftp.Factories
{
    public class FTPSFactory : IFtpFactory
    {
        public IFtp CreateFtp(string source, string destination, string host, string username, string password, int port=21)
        {
            return new FTPSsrv1(source, destination, host, username, password, port);
        }
    }
}
