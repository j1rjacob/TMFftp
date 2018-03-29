using FluentFTP;
using System;
using System.Diagnostics;

namespace TMF_ftp.Helpers
{
	public static class Debug
	{
		class CustomTraceListener : TraceListener
		{
			public override void Write(string message)
			{
				//Console.Write(message);
			}

			public override void WriteLine(string message)
			{
				Console.WriteLine(message);
			}
		}

		public static void LogToCustomListener()
		{
			FtpTrace.AddListener(new CustomTraceListener());
		}
	}
}
