using FluentFTP;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace TMF_ftp.Services
{
	public class FTPSsrv1
	{
		private readonly string _m_host;
		private readonly string _m_user;
		private readonly string _m_pass;

		public FTPSsrv1()
		{
				
		}
		public FTPSsrv1(string m_host, string m_user, string m_pass)
		{
			_m_host = m_host;
			_m_user = m_user;
			_m_pass = m_pass;
		}
		private void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e) => e.Accept = true;

		public FtpClient Connect()
		{
			List<Thread> threads = new List<Thread>();
			FtpClient cl = new FtpClient();

			cl.ValidateCertificate += OnValidateCertificate;
			cl.EncryptionMode = FtpEncryptionMode.Explicit;

			for (int i = 0; i < 1; i++)
			{
				int count = i;

				Thread t = new Thread(delegate ()
				{
					cl.Credentials = new NetworkCredential(_m_user, _m_pass);
					cl.Host = _m_host;
					cl.Connect();

					for (int j = 0; j < 10; j++)
						cl.Execute("NOOP");

					if (count % 2 == 0)
						cl.Disconnect();
				});

				t.Start();
				threads.Add(t);
			}

			while (threads.Count > 0)
			{
				threads[0].Join();
				threads.RemoveAt(0);
			}

			return cl;
		}
	}
}
