using Quartz;
using Quartz.Impl;
using Raccoom.Windows.Forms;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMF_ftp.Factories;
using TMF_ftp.Helpers;
using TMF_ftp.Interfaces;
using TMF_ftp.Models;
using TMF_ftp.Services;
using TMF_ftp.TMFLicensing;
using TMF_ftp.Util;

namespace TMF_ftp
{
    public partial class FormMain : Form
    {
        private static Ftpx _srv;
        private static string _cmbBox;

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static CancellationTokenSource _cancellationTokenSourceFtps = new CancellationTokenSource();
        private static CancellationTokenSource _cancellationTokenSourceSFtp = new CancellationTokenSource();
        private static Thread threadToCancel = null;

        public FormMain()
        {
            _log.Info("Loading Application");

            InitializeComponent();
            Console.SetOut(new ConsoleWriter(richTextBoxDebug));
            ComboBoxConnectionType.SelectedIndex = 0;

            this.tvFileSystem.DataSource = new TreeStrategyFolderBrowserProvider();

            this.tvFileSystem.Populate();
            this.tvFileSystem.Nodes[0].Expand();

            //if (TMFLicense.Validate())
            //{
            //    ButtonDownload.Enabled = true;
            //}
            //else
            //{
            //    MessageBox.Show("License not found. Download is disable");
            //}
        }
        /// <summary>
        /// External method for checking internet access:
        /// </summary>
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        /// <summary>
        /// C# callable method to check internet access
        /// </summary>
        private static bool IsConnectedToInternet()
        {
            return InternetGetConnectedState(out int Description, 0);
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                FirewallManager.SetRule("ON");
                ComboBoxConnectionType.Text = "SFTP";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); //Write to logs
                _log.Error(ex);
                throw;
            }
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

            _srv = new Ftpx()
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
                //Task.Run(() => GetFTPSService(_srv));
            }
            else if (ComboBoxConnectionType.Text == "SFTP")
            {
                Console.WriteLine("Connecting...");
                Console.WriteLine("Try to download");
                //GetSFTPRemoteDirectory();
                //Task.Run(() => GetSFTPService(_srv));
            }
        }
        private void GetSFTPService(Ftpx srv)
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
        private void GetSFTPRemoteDirectory()
        {
            RepeatHere:
            try
            {
                Console.WriteLine("Connecting...");
                TreeStrategyFTPProvider ftpProvider =
                    new TreeStrategyFTPProvider(_srv.Host, _srv.Port, new NetworkCredential(_srv.Username, _srv.Password));

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
        private void GetFTPSRemoteDirectory()
        {
            RepeatHere:
            try
            {
                Console.WriteLine("Connecting...");
                TreeStrategyFTPProvider ftpProvider =
                    new TreeStrategyFTPProvider(_srv.Host, _srv.Port, new NetworkCredential(_srv.Username, _srv.Password));

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
        private void GetFTPSService(Ftpx srv)
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
            IFtp ftp = ftpFactory.CreateFtp("/httpdocs/Test/", "E:\\SecuredFTP\\Test", "stl-amr.com", "j1rjacob", "ajffJNRX143", 21);
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
            _log.Info("Closing the App, Bye.");

            //if (_cmbBox == "FTPS")
            //{
            //    _cancellationTokenSourceFtps.Cancel();
            //    Console.WriteLine("Ftps finish");
            //}
            //else if (_cmbBox == "SFTP")
            //{
            //    _cancellationTokenSourceSFtp.Cancel();
            //    Console.WriteLine("Ftps finish");
            //}

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
        private void ComboBoxConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cmbBox = ComboBoxConnectionType.Text;
        }
        private void ButtonDownload_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Files in the remote folder will be deleted too. Sure?", "TMF ftp", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (_srv != null)
                {
                    PerformDownload();
                    LoadTaskScheduler();
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
                LoadTaskScheduler();
            }
        }
        private static void GoFTPSDownload()
        {
            //while (!_cancellationTokenSourceFtps.IsCancellationRequested)
            //{
            //    if (_cancellationTokenSourceFtps.IsCancellationRequested)
            //    {
            //        Console.WriteLine("Download cancelled");
            //        return;
            //    }
            //    else
            //    {
                    RepeatHere:
                    try
                    {
                        FTPSsrv.Download(_srv);

                        if (FTPSsrv.CheckDirectory(_srv))
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
                        Console.WriteLine(ex.Message);
                        goto RepeatHere;
                    }
            //    }
            //}
        }
        private static void GoSFTPDownload()
        {
            //while (!_cancellationTokenSourceSFtp.IsCancellationRequested)
            //{
            //    if (_cancellationTokenSourceSFtp.IsCancellationRequested)
            //    {
            //        Console.WriteLine("Download cancelled");
            //        return;
            //    }
            //    else
            //    {
                    RepeatHere:
                    try
                    {
                        SFTPsrv.Download(_srv);

                        if (SFTPsrv.CheckDirectory(_srv))
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
                        Console.WriteLine(ex.Message);
                        goto RepeatHere;
                    }
            //    }
            //}
        }
        private void LoadTaskScheduler()
        {
            try
            {
                // construct a scheduler factory
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                // get a scheduler
                IScheduler sched = schedFact.GetScheduler();
                sched.Start();

                IJobDetail job = JobBuilder.Create<AutoJob>()
                    .WithIdentity("myJob", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s
                            //.WithIntervalInHours(1)
                            .WithIntervalInMinutes(60)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 00))
                    )
                    .Build();
                sched.ScheduleJob(job, trigger);
            }
            catch (ArgumentException e)
            {
                _log.Error(e);
            }
        }

        #region ToolStripMenu
        private void toolStripButtonFTPServer_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            if (_cmbBox == "FTPS" && _cancellationTokenSourceFtps != null)
            {
                Console.WriteLine("FTPS");

                _cancellationTokenSourceFtps.Cancel();
            }
            else if (_cmbBox == "SFTP" && _cancellationTokenSourceSFtp != null)
            {
                Console.WriteLine("SFTP");

                _cancellationTokenSourceSFtp.Cancel();
            }
        }
        private void toolStripButtonDisconnect_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButtonLicense_Click(object sender, EventArgs e)
        {
            var validateDialog = new Register();
            validateDialog.evtFrm += new ShowFrm(EnableDownload);
            validateDialog.ShowDialog();
        }
        #endregion
        private void EnableDownload()
        {
            ButtonDownload.Enabled = true;
        }
        public static void PerformDownload()
        {
            if (_cmbBox == "FTPS")
            {
                //_cancellationTokenSourceFtps = new CancellationTokenSource();
                //CancellationToken token = _cancellationTokenSourceFtps.Token;

                //Task t = new Task(() => GoFTPSDownload(), _token);
                //t.Start();
                Task.Factory.StartNew(GoFTPSDownload);
            }
            else if (_cmbBox == "SFTP")
            {
                //_cancellationTokenSourceSFtp = new CancellationTokenSource();
                //CancellationToken token = _cancellationTokenSourceSFtp.Token;

                //Task.Run(() => GoSFTPDownload(token));
                Task.Factory.StartNew(GoSFTPDownload);
            }
        }
    }
}
