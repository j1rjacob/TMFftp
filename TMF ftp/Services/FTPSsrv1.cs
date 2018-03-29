using FluentFTP;
using System;
using System.Net;
using TMF_ftp.Interfaces;

namespace TMF_ftp.Services
{
    public class FTPSsrv1 : IFtp
    {
        public FTPSsrv1()
        {

        }

        public FTPSsrv1(string source, string destination, string host, string username, string password, int port)
        {
            Source = source;
            Destination = destination;
            Host = host;
            Username = username;
            Password = password;
            Port = port;
        }

        public string Source { get; set; }
        public string Destination { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public FtpClient Client { get; set; }

        public void Connect()
        {
            FtpClient client = new FtpClient();

            client.Host = Host;
            client.Credentials = new NetworkCredential(Username, Password); //TODO user input 
            client.EncryptionMode = FtpEncryptionMode.Explicit;
            client.ValidateCertificate += OnValidateCertificate;
            client.Connect();
            Client = client;
        }
        private static void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e) => e.Accept = true;

        public bool DirIsEmpty(string source)
        {
            try
            {
                var files = Client.GetListing(source, FtpListOption.AllFiles);

                foreach (var file in files)
                {
                    if (file.Type == FtpFileSystemObjectType.File)
                    {
                        return true;
                    }
                    else if (file.Type == FtpFileSystemObjectType.Link)
                    {
                        Console.WriteLine("Ignoring symbolic link {0}", file.FullName); //TODO Write to logs
                    }
                    else if (file.Type == FtpFileSystemObjectType.Directory)
                    {
                        DirIsEmpty(file.FullName); //TODO Write to logs
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //TODO Write to logs
            }
            Client.Dispose();
            return false;
        }

        public void DownloadDir()
        {

            Client.Dispose();
        }

        public void DownloadFile()
        {
            throw new NotImplementedException();
        }

        public void DownloadTask()
        {
            throw new NotImplementedException();
        }

        public void DeleteTask()
        {
            throw new NotImplementedException();
        }
    }
}
