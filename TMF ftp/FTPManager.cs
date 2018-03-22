using DBase;
using System;
using System.Windows.Forms;

namespace TMF_ftp
{
    public partial class FTPManager : Form
    {
        public FTPManager()
        {
            InitializeComponent();
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            using (var db = new Db())
            {
                var dto = new FtpConnection()
                {
                    Name = "MDM Ftp", //TODO: Change UI Id
                    Host = "stl-amr.com",
                    Port = 21,
                    Protocol = "FTP",
                    Username = "j1rjacob",
                    Password = "ajffJNRX143",
                    Source = "/",
                    Destination = @"E:\SecuredFTP\Test\",
                    SchedCount = 1,
                    SchedMeasure = "day"
                };

                db.FtpConnections.Add(dto);
                db.SaveChanges();
            }
        }
    }
}
