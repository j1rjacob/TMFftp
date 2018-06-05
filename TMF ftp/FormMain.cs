using QLicense;
using Raccoom.Windows.Forms;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMF_ftp.Helpers;
using TMF_ftp.Models;
using TMF_ftp.Services;
using TMF_ftp.Util;
using TMFLicense;
using TaskScheduler = TMF_ftp.Services.TaskScheduler;

namespace TMF_ftp
{
    public partial class FormMain : Form
    {
        private static BaseFtp _baseFtp;
        private static string _cmbBox;
        private static string _sourcePath = @"E:\SecuredFTP\Test";

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static CancellationTokenSource _cancellationTokenSourceFtps = new CancellationTokenSource();
        private static CancellationTokenSource _cancellationTokenSourceSFtp = new CancellationTokenSource();
        private static Thread threadToCancel = null;
        byte[] _certPubicKeyData;
        
        public FormMain()
        {
            _log.Info("Loading Application");

            InitializeComponent();
            Console.SetOut(new ConsoleWriter(richTextBoxDebug));
            ComboBoxConnectionType.SelectedIndex = 0;

            PopulateFTPBrowser();
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        private static bool IsConnectedToInternet()
        {
            return InternetGetConnectedState(out int Description, 0);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadDefaultFTP();
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            if (IsConnectedToInternet() == false)
            {
                MessageBox.Show("Not connected to internet", "TMF ftp");
                return;
            }

            if (string.IsNullOrEmpty(TextBoxHost.Text) ||
                string.IsNullOrEmpty(TextBoxUsername.Text) ||
                string.IsNullOrEmpty(TextBoxPassword.Text) ||
                string.IsNullOrEmpty(TextBoxPort.Text) ||
                string.IsNullOrEmpty(TextBoxRemote.Text) ||
                string.IsNullOrEmpty(TextBoxDestination.Text) ||
                string.IsNullOrEmpty(ComboBoxConnectionType.Text)
               )
            {
                MessageBox.Show("Could not connect, please check server info.", "TMF ftp");
                return;
            }

            _baseFtp = new BaseFtp()
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

            if (ComboBoxConnectionType.Text == "FTPS")
            {
                Console.WriteLine("Connecting...");
                GetFTPSRemoteDirectory();
            }
            else if (ComboBoxConnectionType.Text == "SFTP")
            {
                Console.WriteLine("Connecting...");
                Thread.Sleep(2000);//LOL
                Console.WriteLine("Try to download");
            }
        }

        private void GetFTPSRemoteDirectory()
        {
            RepeatHere:
            try
            {
                Console.WriteLine("Connecting...");
                TreeStrategyFTPProvider ftpProvider =
                    new TreeStrategyFTPProvider(_baseFtp.Host, _baseFtp.Port, new NetworkCredential(_baseFtp.Username, _baseFtp.Password));

                tvFolderBrowserSource.DataSource = ftpProvider;
                tvFolderBrowserSource.Populate();
                tvFolderBrowserSource.Nodes[0].Expand();
                Console.WriteLine("Connected.");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                goto RepeatHere;
            }
        }

        private void richTextBoxDebug_TextChanged(object sender, EventArgs e)
        {
            richTextBoxDebug.SelectionStart = richTextBoxDebug.Text.Length;
            richTextBoxDebug.ScrollToCaret();
        }

        protected override void OnClosed(EventArgs e)
        {
            new FirewallManager().RemoveFirewallRule();
            _log.Info("Closing the App, Bye.");

            base.OnClosed(e);
            Application.Exit();
            Environment.Exit(0);
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
                Console.WriteLine(exception);
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

        private void ComboBoxConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cmbBox = ComboBoxConnectionType.Text;
        }

        private void ButtonDownload_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Files in the remote folder will be deleted too. Sure?", "TMF ftp", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (_baseFtp != null)
                {
                    _sourcePath = TextBoxDestination.Text;
                    PerformDownload();
                    new TaskScheduler().Perform();
                }
                else
                    MessageBox.Show("Connect first");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void CheckBoxAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAuto.Checked)
            {
                new TaskScheduler().Perform();
            }
        }

        private static void GoFTPSDownload()
        {
            RepeatHere:
            try
            {
                new FTPSsrv().Download(_baseFtp);

                if (new FTPSsrv().CheckDirectory(_baseFtp))
                {
                    goto RepeatHere;
                }
                Console.WriteLine("Download Finished");
                
                new Latest().UpdateOnOMSAndRDS();
            }
            catch (IOException)
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

        private static void GoSFTPDownload()
        {
            RepeatHere:
            try
            {
                new SFTPsrv().Download(_baseFtp);

                if (new SFTPsrv().CheckDirectory(_baseFtp))
                {
                    goto RepeatHere;
                }
                Console.WriteLine("Download Finished");
                
                new Latest().UpdateOnOMSAndRDS();
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

        #region Methods
        private void PopulateFTPBrowser()
        {
            this.tvFileSystem.DataSource = new TreeStrategyFolderBrowserProvider();
            this.tvFileSystem.Populate();
            this.tvFileSystem.Nodes[0].Expand();
        }

        private void LoadDefaultFTP()
        {
            try
            {
                new FirewallManager().SetRule("ON");
                ComboBoxConnectionType.Text = "SFTP";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _log.Error(ex);
                throw;
            }
        }
        #endregion

        public static void PerformDownload()
        {
            if (_cmbBox == "FTPS")
            {
                Task.Factory.StartNew(GoFTPSDownload);
            }
            else if (_cmbBox == "SFTP")
            {
                Task.Factory.StartNew(GoSFTPDownload);
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            CheckLicense();
        }

        private void CheckLicense()
        {
            //Initialize variables with default values
            MyLicense _lic = null;
            string _msg = string.Empty;
            LicenseStatus _status = LicenseStatus.UNDEFINED;

            //Read public key from assembly
            Assembly _assembly = Assembly.GetExecutingAssembly();
            using (MemoryStream _mem = new MemoryStream())
            {
                _assembly.GetManifestResourceStream("TMF_ftp.LicenseVerify.cer").CopyTo(_mem);
                _certPubicKeyData = _mem.ToArray();
            }

            //Check if the XML license file exists
            if (File.Exists("license.lic"))
            {
                _lic = (MyLicense)LicenseHandler.ParseLicenseFromBASE64String(
                    typeof(MyLicense),
                    File.ReadAllText("license.lic"),
                    _certPubicKeyData,
                    out _status,
                    out _msg);
            }
            else
            {
                _status = LicenseStatus.INVALID;
                _msg = "This application is not activated";
            }

            switch (_status)
            {
                case LicenseStatus.VALID:
                    //TODO: If license is valid, you can do extra checking here
                    //TODO: E.g., check license expiry date if you have added expiry date property to your license entity
                    //TODO: Also, you can set feature switch here based on the different properties you added to your license entity 

                    return;

                default:

                    MessageBox.Show(_msg, "TMF ftp", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    using (frmActivation frm = new frmActivation())
                    {
                        frm.CertificatePublicKeyData = _certPubicKeyData;
                        frm.ShowDialog();

                        Application.Exit();
                    }
                    break;
            }
        }
    }
}
