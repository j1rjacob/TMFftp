using FluentFTP;
using System;
using System.IO;
using System.Net;
using TMF_ftp.Helpers;
using TMF_ftp.Models;

namespace TMF_ftp.Services
{
    public class FTPSsrv
    {
        public FTPSsrv()
        {
            new Debug().LogToCustomListener();
        }

        public void Connect(BaseFtp srv)
        {
            using (var client = new FtpClient())
            {
                client.Host = srv.Host;
                client.Credentials = new NetworkCredential(srv.Username, srv.Password); //TODO user input 
                client.EncryptionMode = FtpEncryptionMode.Explicit;
                client.ValidateCertificate += OnValidateCertificate;
                //client.SocketKeepAlive = true;
                client.Connect();
            }
        }

        public void Download(BaseFtp srv)
        {
            using (FtpClient client = new FtpClient())
            {
                client.Host = srv.Host;
                client.Credentials = new NetworkCredential(srv.Username, srv.Password); //TODO user input 
                client.EncryptionMode = FtpEncryptionMode.Explicit;
                client.ValidateCertificate += OnValidateCertificate;
                //client.SocketKeepAlive = true;
                client.Connect();

                DownloadDirectory(client, srv.RemoteDirectory, srv.LocalDirectory);
            }
        }

        public bool CheckDirectory(BaseFtp srv)
        {
            using (FtpClient client = new FtpClient())
            {
                client.Host = srv.Host;
                client.Credentials = new NetworkCredential(srv.Username, srv.Password); //TODO user input 
                client.EncryptionMode = FtpEncryptionMode.Explicit;
                client.ValidateCertificate += OnValidateCertificate;
                //client.SocketKeepAlive = true;
                client.Connect();

                return IsEmpty(client, srv.RemoteDirectory);
            }
        }

        private void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e) => e.Accept = true;

        public void DownloadDirectory(FtpClient client, string source, string destination)
        {
            try
            {
                var files = client.GetListing(source, FtpListOption.AllFiles);

                foreach (var file in files)
                {
                    if (file.Type == FtpFileSystemObjectType.File)
                    {
                        DownloadFile(client, file, destination);
                    }
                    else if (file.Type == FtpFileSystemObjectType.Link)
                    {
                        Console.WriteLine("Ignoring symbolic link {0}", file.FullName); //TODO Write to logs
                    }
                    else if (file.Type == FtpFileSystemObjectType.Directory)
                    {
                        var dir = Directory.CreateDirectory(Path.Combine(destination, file.Name));
                        try
                        {
                            if (!Directory.Exists(dir.FullName))
                            {
                                Directory.CreateDirectory(Path.Combine(destination, file.Name));
                                Console.WriteLine($"Directory created: {file.Name}");
                            }
                            DownloadDirectory(client, file.FullName, dir.FullName); //TODO Write to logs
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //TODO Write to logs
            }
        }

        private void DownloadFile(FtpClient client, FtpListItem file, string destination)
        {
            try
            {
                client.DownloadFile(destination + "\\" + file.Name, file.FullName, true, FtpVerify.OnlyChecksum);
                Console.WriteLine($"Successful download: {file.FullName}"); //TODO Write to logs
                client.DeleteFile(file.FullName);
                new BulkInsert().Perform(@"E:\SecuredFTP\Test");
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //TODO Write to logs
                //throw;
            }
        }

        public bool IsEmpty(FtpClient client, string source)
        {
            try
            {
                var files = client.GetListing(source, FtpListOption.AllFiles);

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
                        IsEmpty(client, file.FullName); //TODO Write to logs
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //TODO Write to logs
            }
            return false;
        }
    }
}
