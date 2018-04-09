using FluentFTP;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.IO;
using TMF_ftp.Models;

namespace TMF_ftp.Services
{
    public static class SFTPsrv
	{
	    static SFTPsrv()
	    {
            Helpers.Debug.LogToCustomListener();

        }
        public static void Connect(Ftpx srv)
		{
			using (var client = new SftpClient(srv.Host, srv.Port, srv.Username, srv.Password))
			{
                client.KeepAliveInterval = TimeSpan.FromHours(1);
				client.Connect();
                System.Diagnostics.Debug.WriteLine("Successful!");
			}
		}
		public static void Download(Ftpx srv)
		{
			using (var sftp = new SftpClient(srv.Host, srv.Port, srv.Username, srv.Password))
			{
				sftp.Connect();
				DownloadDirectory(sftp, sftp.WorkingDirectory, srv.LocalDirectory);
			}
		}
	    public static bool CheckDirectory(Ftpx srv)
	    {
	        using (var sftp = new SftpClient(srv.Host, srv.Port, srv.Username, srv.Password))
	        {
	            sftp.Connect();
	            return IsEmpty(sftp, srv.RemoteDirectory);
            }
        }
	    private static void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e) => e.Accept = true;
        private static void DownloadDirectory(SftpClient client, string source, string destination)
		{
			try
			{
				var files = client.ListDirectory(source);
				foreach (var file in files)
				{
					if (!file.IsDirectory && !file.IsSymbolicLink)
					{
						DownloadFile(client, file, destination);
					}
					else if (file.IsSymbolicLink)
					{
						Console.WriteLine("Ignoring symbolic link {0}", file.FullName);
					}
					else if (file.Name != "." && file.Name != "..")
					{
						var dir = Directory.CreateDirectory(Path.Combine(destination, file.Name));
						Console.WriteLine($"Directory created: {file.Name}"); //TODO: Write to logs
						DownloadDirectory(client, file.FullName, dir.FullName);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e); //TODO: Write to logs
				throw;
			}
		}
		private static void DownloadFile(SftpClient client, SftpFile file, string directory)
		{
			try
			{
				using (Stream fileStream = File.OpenWrite(Path.Combine(directory, file.Name)))
				{
					client.DownloadFile(file.FullName, fileStream);
					Console.WriteLine($"Successful download: {file.FullName}"); //TODO Write to logs
				}

				client.DeleteFile(file.FullName);
				Console.WriteLine($"Immediately deleted : {file.FullName}"); //TODO Write to logs
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	    public static bool IsEmpty(SftpClient client, string source)
	    {
	        try
	        {
	            var files = client.ListDirectory(source);

                foreach (var file in files)
	            {
	                if (!file.IsDirectory && !file.IsSymbolicLink)
                    {
	                    return true;
	                }
	                else if (file.IsSymbolicLink)
                    {
	                    Console.WriteLine("Ignoring symbolic link {0}", file.FullName); //TODO Write to logs
	                }
	                else if (file.Name != "." && file.Name != "..")
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
