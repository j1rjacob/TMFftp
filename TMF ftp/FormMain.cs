using FluentFTP;
using Raccoom.Windows.Forms;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMF_ftp.Factories;
using TMF_ftp.Helpers;
using TMF_ftp.Interfaces;
using TMF_ftp.Models;
using TMF_ftp.Services;

namespace TMF_ftp
{
    public partial class FormMain : Form
    {
        private Ftps _srv;
        private FtpClient _conn;

        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;
        private Task _task;

        public FormMain()
        {
            InitializeComponent();
            Console.SetOut(new ConsoleWriter(richTextBoxDebug));
	        ComboBoxConnectionType.SelectedIndex = 0;

            this.tvFileSystem.DataSource =  new TreeStrategyFolderBrowserProvider();

            this.tvFileSystem.Populate();
            this.tvFileSystem.Nodes[0].Expand();
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
	        _srv = new Ftps()
	        {
		        Host = TextBoxHost.Text,
		        Username = TextBoxUsername.Text,
		        Password = TextBoxPassword.Text,
		        Port = Convert.ToInt32(TextBoxPort.Text),
		        Type = ComboBoxConnectionType.SelectedText,
		        Auto = CheckBoxAuto.Checked,
		        RemoteDirectory = TextBoxRemote.Text,
		        LocalDirectory = TextBoxDestination.Text
	        };
            GetRemoteDirectory();
            Task.Run(() => GetFTPSService(_srv));
            //SFTPsrv.Download();
        }
        private void GetRemoteDirectory()
        {
            RepeatHere:
            try
            {
                TreeStrategyFTPProvider ftpProvider =
                    new TreeStrategyFTPProvider(_srv.Host, _srv.Port, new NetworkCredential(_srv.Username, _srv.Password));

                tvFolderBrowserSource.DataSource = ftpProvider;
                tvFolderBrowserSource.Populate();
                tvFolderBrowserSource.Nodes[0].Expand();
            }
            catch (Exception)
            {
                goto RepeatHere;
            }
        }
        private void GetFTPSService(Ftps srv)
        {
			RepeatHere:
            try
            {
                //FTPSsrv.Connect(_srv);
                
                //TODO: Check all folder is empty.
                if (FTPSsrv.CheckDirectory(srv))
                {
                    goto RepeatHere;
                }
                
                //Console.WriteLine("Download Finished");
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
        }
        public static void Test()
        {
            IFtpFactory ftpFactory = new FTPSFactory();
            IFtp ftp = ftpFactory.CreateFtp("/httpdocs/Test/", "E:\\SecuredFTP\\Test", "stl-amr.com","j1rjacob","ajffJNRX143", 21);
            ftp.Connect();
            ftp.DownloadDir();
            ftp.DirIsEmpty("/httpdocs/Test/");
        }
        private void richTextBoxDebug_TextChanged(object sender, EventArgs e)
        {
            richTextBoxDebug.SelectionStart = richTextBoxDebug.Text.Length;
            richTextBoxDebug.ScrollToCaret();
        }
        protected override void OnClosed(EventArgs e)
        {
            FirewallManager.RemoveFirewallRule();
            _tokenSource.Cancel();
            base.OnClosed(e);
            Application.Exit();
        }
        private void tvFileSystem_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNodePath node = e.Node as TreeNodePath;
                if (node != null) TextBoxDestination.Text = string.Format(node.Path);
            }
            catch (Exception exception)
            {
                //Console.WriteLine(exception);
                //throw;
                return;
            }
        }
        private void tvFolderBrowserSource_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNodePath node = e.Node as TreeNodePath;
                if (node != null) TextBoxRemote.Text = string.Format(node.Path).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //throw;
                return;
            }
        }
        private void ButtonDownload_Click(object sender, EventArgs e)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            _task = Task.Run(() => GoDownload(), _token);
        }
        private void GoDownload()
        {
            RepeatHere:
            try
            {
                FTPSsrv.Download(_srv);

                //TODO: Check all folder is empty.
                if (FTPSsrv.CheckDirectory(_srv))
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
        }
    }
}
