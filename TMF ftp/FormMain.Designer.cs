namespace TMF_ftp
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBoxHost = new System.Windows.Forms.TextBox();
            this.TextBoxUsername = new System.Windows.Forms.TextBox();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.TextBoxPort = new System.Windows.Forms.TextBox();
            this.ButtonPlay = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ComboBoxConnectionType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tvFolderBrowserSource = new Raccoom.Windows.Forms.TreeViewFolderBrowser();
            this.label8 = new System.Windows.Forms.Label();
            this.TextBoxRemote = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ButtonDownload = new System.Windows.Forms.Button();
            this.tvFileSystem = new Raccoom.Windows.Forms.TreeViewFolderBrowser();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label7 = new System.Windows.Forms.Label();
            this.TextBoxDestination = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CheckBoxAuto = new System.Windows.Forms.CheckBox();
            this.ButtonCache = new System.Windows.Forms.Button();
            this.richTextBoxDebug = new System.Windows.Forms.RichTextBox();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(144, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(312, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(480, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Type:";
            // 
            // TextBoxHost
            // 
            this.TextBoxHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxHost.Location = new System.Drawing.Point(40, 8);
            this.TextBoxHost.Name = "TextBoxHost";
            this.TextBoxHost.Size = new System.Drawing.Size(100, 21);
            this.TextBoxHost.TabIndex = 6;
            this.TextBoxHost.Text = "stl-amr.com";
            // 
            // TextBoxUsername
            // 
            this.TextBoxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxUsername.Location = new System.Drawing.Point(208, 8);
            this.TextBoxUsername.Name = "TextBoxUsername";
            this.TextBoxUsername.Size = new System.Drawing.Size(100, 21);
            this.TextBoxUsername.TabIndex = 7;
            this.TextBoxUsername.Text = "j1rjacob";
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxPassword.Location = new System.Drawing.Point(376, 8);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '*';
            this.TextBoxPassword.Size = new System.Drawing.Size(100, 21);
            this.TextBoxPassword.TabIndex = 8;
            this.TextBoxPassword.Text = "ajffJNRX143";
            // 
            // TextBoxPort
            // 
            this.TextBoxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxPort.Location = new System.Drawing.Point(512, 8);
            this.TextBoxPort.Name = "TextBoxPort";
            this.TextBoxPort.Size = new System.Drawing.Size(40, 21);
            this.TextBoxPort.TabIndex = 9;
            this.TextBoxPort.Text = "21";
            // 
            // ButtonPlay
            // 
            this.ButtonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPlay.Location = new System.Drawing.Point(560, 7);
            this.ButtonPlay.Name = "ButtonPlay";
            this.ButtonPlay.Size = new System.Drawing.Size(75, 23);
            this.ButtonPlay.TabIndex = 10;
            this.ButtonPlay.Text = "connect";
            this.ButtonPlay.UseVisualStyleBackColor = true;
            this.ButtonPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ComboBoxConnectionType
            // 
            this.ComboBoxConnectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBoxConnectionType.FormattingEnabled = true;
            this.ComboBoxConnectionType.Items.AddRange(new object[] {
            "FTPS",
            "SFTP"});
            this.ComboBoxConnectionType.Location = new System.Drawing.Point(40, 34);
            this.ComboBoxConnectionType.Name = "ComboBoxConnectionType";
            this.ComboBoxConnectionType.Size = new System.Drawing.Size(100, 23);
            this.ComboBoxConnectionType.TabIndex = 15;
            this.ComboBoxConnectionType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxConnectionType_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Chocolate;
            this.panel2.Controls.Add(this.tvFolderBrowserSource);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.TextBoxRemote);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(1, 201);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(720, 177);
            this.panel2.TabIndex = 17;
            // 
            // tvFolderBrowserSource
            // 
            this.tvFolderBrowserSource.CheckBoxBehaviorMode = Raccoom.Windows.Forms.CheckBoxBehaviorMode.None;
            this.tvFolderBrowserSource.DataSource = null;
            this.tvFolderBrowserSource.HideSelection = false;
            this.tvFolderBrowserSource.Location = new System.Drawing.Point(360, 32);
            this.tvFolderBrowserSource.Name = "tvFolderBrowserSource";
            this.tvFolderBrowserSource.PathSeparator = "/";
            this.tvFolderBrowserSource.ShowLines = false;
            this.tvFolderBrowserSource.ShowRootLines = false;
            this.tvFolderBrowserSource.Size = new System.Drawing.Size(360, 144);
            this.tvFolderBrowserSource.TabIndex = 21;
            this.tvFolderBrowserSource.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFolderBrowserSource_AfterSelect);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Location = new System.Drawing.Point(364, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "Source:";
            // 
            // TextBoxRemote
            // 
            this.TextBoxRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxRemote.Location = new System.Drawing.Point(448, 8);
            this.TextBoxRemote.Name = "TextBoxRemote";
            this.TextBoxRemote.Size = new System.Drawing.Size(264, 21);
            this.TextBoxRemote.TabIndex = 18;
            this.TextBoxRemote.Text = "/httpdocs/Test/";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Maroon;
            this.panel4.Controls.Add(this.ButtonDownload);
            this.panel4.Controls.Add(this.tvFileSystem);
            this.panel4.Controls.Add(this.treeView1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.TextBoxDestination);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(360, 177);
            this.panel4.TabIndex = 17;
            // 
            // ButtonDownload
            // 
            this.ButtonDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDownload.Location = new System.Drawing.Point(286, 7);
            this.ButtonDownload.Name = "ButtonDownload";
            this.ButtonDownload.Size = new System.Drawing.Size(72, 23);
            this.ButtonDownload.TabIndex = 19;
            this.ButtonDownload.Text = "download";
            this.ButtonDownload.UseVisualStyleBackColor = true;
            this.ButtonDownload.Click += new System.EventHandler(this.ButtonDownload_Click);
            // 
            // tvFileSystem
            // 
            this.tvFileSystem.CheckBoxBehaviorMode = Raccoom.Windows.Forms.CheckBoxBehaviorMode.None;
            this.tvFileSystem.DataSource = null;
            this.tvFileSystem.HideSelection = false;
            this.tvFileSystem.Location = new System.Drawing.Point(0, 32);
            this.tvFileSystem.Name = "treeViewFolderBrowser1";
            this.tvFileSystem.ShowLines = false;
            this.tvFileSystem.ShowRootLines = false;
            this.tvFileSystem.Size = new System.Drawing.Size(360, 144);
            this.tvFileSystem.TabIndex = 18;
            this.tvFileSystem.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFileSystem_AfterSelect);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.treeView1.Location = new System.Drawing.Point(0, 33);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(360, 144);
            this.treeView1.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(8, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Destination:";
            // 
            // TextBoxDestination
            // 
            this.TextBoxDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDestination.Location = new System.Drawing.Point(92, 8);
            this.TextBoxDestination.Name = "TextBoxDestination";
            this.TextBoxDestination.Size = new System.Drawing.Size(188, 21);
            this.TextBoxDestination.TabIndex = 14;
            this.TextBoxDestination.Text = "E:\\SecuredFTP\\Test";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 387);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(723, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.Visible = false;
            // 
            // CheckBoxAuto
            // 
            this.CheckBoxAuto.AutoSize = true;
            this.CheckBoxAuto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CheckBoxAuto.Checked = true;
            this.CheckBoxAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxAuto.Location = new System.Drawing.Point(141, 40);
            this.CheckBoxAuto.Name = "CheckBoxAuto";
            this.CheckBoxAuto.Size = new System.Drawing.Size(82, 19);
            this.CheckBoxAuto.TabIndex = 20;
            this.CheckBoxAuto.Text = "Allow Auto";
            this.CheckBoxAuto.UseVisualStyleBackColor = true;
            this.CheckBoxAuto.CheckedChanged += new System.EventHandler(this.CheckBoxAuto_CheckedChanged);
            // 
            // ButtonCache
            // 
            this.ButtonCache.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonCache.BackgroundImage")));
            this.ButtonCache.Enabled = false;
            this.ButtonCache.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCache.Location = new System.Drawing.Point(640, 7);
            this.ButtonCache.Name = "ButtonCache";
            this.ButtonCache.Size = new System.Drawing.Size(24, 23);
            this.ButtonCache.TabIndex = 12;
            this.ButtonCache.UseVisualStyleBackColor = true;
            this.ButtonCache.Visible = false;
            // 
            // richTextBoxDebug
            // 
            this.richTextBoxDebug.BackColor = System.Drawing.SystemColors.MenuText;
            this.richTextBoxDebug.ForeColor = System.Drawing.SystemColors.Window;
            this.richTextBoxDebug.Location = new System.Drawing.Point(1, 64);
            this.richTextBoxDebug.Name = "richTextBoxDebug";
            this.richTextBoxDebug.Size = new System.Drawing.Size(720, 136);
            this.richTextBoxDebug.TabIndex = 2;
            this.richTextBoxDebug.Text = "";
            this.richTextBoxDebug.TextChanged += new System.EventHandler(this.richTextBoxDebug_TextChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 409);
            this.Controls.Add(this.richTextBoxDebug);
            this.Controls.Add(this.CheckBoxAuto);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ComboBoxConnectionType);
            this.Controls.Add(this.ButtonCache);
            this.Controls.Add(this.ButtonPlay);
            this.Controls.Add(this.TextBoxPort);
            this.Controls.Add(this.TextBoxPassword);
            this.Controls.Add(this.TextBoxUsername);
            this.Controls.Add(this.TextBoxHost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TMF ftp";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBoxHost;
        private System.Windows.Forms.TextBox TextBoxUsername;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.TextBox TextBoxPort;
        private System.Windows.Forms.Button ButtonPlay;
        private System.Windows.Forms.Button ButtonCache;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox ComboBoxConnectionType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.CheckBox CheckBoxAuto;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox TextBoxRemote;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox TextBoxDestination;
        private System.Windows.Forms.TreeView treeView1;
        private Raccoom.Windows.Forms.TreeViewFolderBrowser tvFileSystem;
        private Raccoom.Windows.Forms.TreeViewFolderBrowser tvFolderBrowserSource;
        private System.Windows.Forms.Button ButtonDownload;
        private System.Windows.Forms.RichTextBox richTextBoxDebug;
    }
}

