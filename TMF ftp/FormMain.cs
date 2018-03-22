using System;
using System.Windows.Forms;
using TMF_ftp.Helpers;
using TMF_ftp.Services;

namespace TMF_ftp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            //FirewallManager.SetRule("ON");
            try
            {
                FirewallManager.SetRule("ON");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception); //Write to logs
                throw;
            }
        }
        private void ButtonPlay_Click(object sender, EventArgs e)
		{
			RepeatHere:
			try
			{
				FTPSsrv.Connect();
			}
			catch (System.Net.Sockets.SocketException)
			{
				goto RepeatHere;
			}
			catch (Exception ex)
			{
				//Console.WriteLine(ex.Message);
				if (ex.Message == "Timed out trying to connect!")
					goto RepeatHere;
				else
					Console.WriteLine(ex.Message);
				//return;
			}
			//SFTPsrv.Download();
		}

        protected override void OnClosed(EventArgs e)
        {
            FirewallManager.RemoveFirewallRule();
            base.OnClosed(e);
            Application.Exit();
        }
    }
}
