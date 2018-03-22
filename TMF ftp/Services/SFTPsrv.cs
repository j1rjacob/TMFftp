using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Diagnostics;
using System.IO;

namespace TMF_ftp.Services
{
	public class SFTPsrv
	{

		public static void Connect()
		{
			using (var client = new SftpClient("127.0.0.1", 22, "j1rjacob", "12345678"))
			{
				client.Connect();
				Debug.WriteLine("Successful!");
			}
		}

		public static void Download()
		{
			string host = @"127.0.0.1";
			int port = 22;
			string username = "j1rjacob";
			string password = "ajffJNRX143";

			string remoteDirectory = "/JizFTP/";
			string localDirectory = @"E:\SecuredFTP\Test\";

			using (var sftp = new SftpClient(host, port, username, password))
			{
				sftp.Connect();
				DownloadDirectory(sftp, sftp.WorkingDirectory, localDirectory);
				Console.WriteLine("Kalas");
				//var files = sftp.ListDirectory(sftp.WorkingDirectory + "/84EB18E26184/");

				//foreach (var file in files)
				//{
				//    string remoteFileName = file.Name;
				//    Console.WriteLine(remoteFileName);
				//    //if ((!file.Name.StartsWith(".")) && ((file.LastWriteTime.Date == DateTime.Today)))

				//        using (Stream file1 = File.OpenWrite(localDirectory + remoteFileName))
				//        {
				//            sftp.DownloadFile(sftp.WorkingDirectory + "/84EB18E26184/" + remoteFileName, file1);
				//            Console.WriteLine("Ok");
				//        }
				//}
			}
		}

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
	}
}
