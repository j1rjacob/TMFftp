using FluentFTP;
using Raccoom.Windows.Forms;
using System;
using System.Net;
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
        public FormMain()
        {
            InitializeComponent();
            Console.SetOut(new ConsoleWriter(richTextBoxDebug));
	        ComboBoxConnectionType.SelectedIndex = 0;

            this.tvFileSystem.DataSource =  new TreeStrategyFolderBrowserProvider();

            // fill root level
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
	        var srv = new Ftps()
	        {
		        Host = TextBoxHost.Text,
		        Username = TextBoxUsername.Text,
		        Password = TextBoxPassword.Text,
		        Port = TextBoxPort.Text,
		        Type = ComboBoxConnectionType.SelectedText,
		        Auto = CheckBoxAuto.Checked,
		        RemoteDirectory = TextBoxRemote.Text,
		        LocalDirectory = TextBoxDestination.Text
	        };

	        using (FtpClient client = new FtpClient())
	        {
		        client.Host = srv.Host;
		        client.Credentials = new NetworkCredential(srv.Username, srv.Password); //TODO user input 
		        client.EncryptionMode = FtpEncryptionMode.Explicit;
		        client.ValidateCertificate += OnValidateCertificate;
		        client.Connect();

				GetDirectory(client, srv.RemoteDirectory, srv.LocalDirectory);
			}

			Task.Run(() => GetFTPSService(srv));
            //SFTPsrv.Download();
        }
	    private static void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e) => e.Accept = true;

        private void GetDirectory(FtpClient client, string source, string destination)
        {
            try
            {
                var files = client.GetListing(source, FtpListOption.AllFiles);
                
                foreach (var file in files)
                {
                    ListViewItem item = new ListViewItem();
                    
                    if (file.Type == FtpFileSystemObjectType.File)
                    {
                        item.Text = file.Name;
                        item.SubItems.Add("File");
                        item.SubItems.Add(file.FullName);
                        item.SubItems.Add(file.Size.ToString());
                        ListViewSource.Items.Add(item);
                    }
                    else if (file.Type == FtpFileSystemObjectType.Directory)
                    {
                        item.Text = file.Name;
                        item.SubItems.Add("Folder");
                        item.SubItems.Add(file.FullName);
                        item.SubItems.Add(file.Size.ToString());
                        ListViewSource.Items.Add(item);
                        GetDirectory(client, file.FullName, destination + file.Name); //TODO Write to logs
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e); //TODO Write to logs
            }
        }

        private void GetFTPSService(Ftps srv)
        {
			RepeatHere:
            try
            {
                FTPSsrv.Connect(srv);
                //TODO: Check all folder is empty.
                if (FTPSsrv.CheckDirectory(srv))
                {
                    goto RepeatHere;
                }
                
                Console.WriteLine("Download Finished");
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
                //Console.WriteLine(ex.Message);
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
            base.OnClosed(e);
            Application.Exit();
        }

        private void tvFileSystem_SelectedDirectoriesChanged(object sender, SelectedDirectoriesChangedEventArgs e)
        {
            //try
            //{
            //    TextBoxDestination.Text = tvFileSystem.SelectedNode.FullPath;
            //}
            //catch (Exception)
            //{
            //    //Console.WriteLine(ex);
            //    //throw;
            //    return;
            //}
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
    }
}
