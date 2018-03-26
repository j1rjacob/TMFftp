using FluentFTP;
using System;
using System.IO;
using System.Net;

namespace TMF_ftp.Services
{
    public static class FTPSsrv
    {
        public static void Connect()
        {
            string localDirectory = @"E:\SecuredFTP\Test\"; //TODO user input path

            using (FtpClient client = new FtpClient())
            {
                client.Host = "stl-amr.com";
                client.Credentials = new NetworkCredential("j1rjacob", "ajffJNRX143"); //TODO user input 
                client.EncryptionMode = FtpEncryptionMode.Explicit;
                client.ValidateCertificate += OnValidateCertificate;
                client.Connect();

                
                //TODO: Change source to dynamic path
                DownloadDirectory(client, "/httpdocs/Test/", localDirectory);

                //var folders = client.GetListing("/httpdocs/UploadedFiles", FtpListOption.AllFiles);
                //foreach (var folder in folders)
                //{
                //    DownloadDirectory(client, folder, localDirectory);
                //}
                //client.DownloadFile(@"C:\Test\NEWBACKUP.zip", "/httpdocs/NEWBACKUP.zip",true,FtpVerify.OnlyChecksum);
                //client.DeleteDirectory("/httpdocs/Test/");
            }
        }
        public static bool CheckDirectory()
        {
            string localDirectory = @"E:\SecuredFTP\Test\"; //TODO user input path

            using (FtpClient client = new FtpClient())
            {
                client.Host = "stl-amr.com";
                client.Credentials = new NetworkCredential("j1rjacob", "ajffJNRX143"); //TODO user input 
                client.EncryptionMode = FtpEncryptionMode.Explicit;
                client.ValidateCertificate += OnValidateCertificate;
                client.Connect();
                
                if (IsEmpty(client, "/httpdocs/Test/"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private static void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e) => e.Accept = true;

        private static void DownloadDirectory(FtpClient client, string source, string destination)
        {
            try
            {
                var files = client.GetListing(source, FtpListOption.AllFiles);

                foreach (var file in files)
                {
                    if (file.Type == FtpFileSystemObjectType.File)
                    {
                        DownloadFile(client, file, destination);
                        //await Task.Run(() => DownloadFile(client, file, destination));
                    }
                    else if (file.Type == FtpFileSystemObjectType.Link)
                    {
                        Console.WriteLine("Ignoring symbolic link {0}", file.FullName); //TODO Write to logs
                    }
                    else if (file.Type == FtpFileSystemObjectType.Directory)
                    {
                        var dir = Directory.CreateDirectory(Path.Combine(destination, file.Name));
                        Console.WriteLine($"Directory created: {file.Name}");
                        DownloadDirectory(client, file.FullName, dir.FullName); //TODO Write to logs
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //TODO Write to logs
            }
        }

        public static bool IsEmpty(FtpClient client, string source)
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

        private static void DownloadFile(FtpClient client, FtpListItem file, string destination)
        {
            try
            {
                DownloadFileTask(client, file, destination);
                DeleteFileTask(client, file);
                //var t1 = new Task(() => DownloadFileTask(client, file, destination)); 
                //t1.ContinueWith((t) => DeleteFileTask(client, file));
                //t1.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //TODO Write to logs
                                      //throw;
            }
        }

        private static void DeleteFileTask(FtpClient client, FtpListItem file)
        {
            client.DeleteFile(file.FullName);
            Console.WriteLine($"Immediately deleted : {file.FullName}"); //TODO Write to logs
        }

        private static void DownloadFileTask(FtpClient client, FtpListItem file, string destination)
        {
            client.DownloadFile(destination + "\\" + file.Name, file.FullName, true, FtpVerify.OnlyChecksum);
            Console.WriteLine($"Successful download: {file.FullName}"); //TODO Write to logs
        }
    }
}
