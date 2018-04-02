namespace TMF_ftp.Models
{
    public class Ftpx
	{
		public string Host { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public int Port { get; set; }
		public string Type { get; set; }
		public bool Auto { get; set; }
		public string LocalDirectory { get; set; }
		public string RemoteDirectory { get; set; }
	}
}
