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

                //TODO: Check all folder is empty.
		        if (FTPSsrv.CheckDirectory())
		        {
		            goto RepeatHere;
                }
		    }
		    catch (System.IO.IOException)
		    {
		        goto RepeatHere;
		    }
		    catch (System.Net.Sockets.SocketException)
		    {
		        goto RepeatHere;
		    }
            catch (TimeoutException)
		    {
		        goto RepeatHere;
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			    goto RepeatHere;
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
