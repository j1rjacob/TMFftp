using System;
using System.IO;
using System.Windows.Forms;

namespace TMF_ftp
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (openFileDialogRDS.ShowDialog() == DialogResult.OK)
            //    {
            //        //BulkRDS.Import(openFileDialogRDS.FileNames);
            //        //BulkOMS.Import(openFileDialogRDS.FileNames);
            //    }
            //    //MessageBox.Show("Import was successful");
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Import was not successful");
            //}

            FormMain.BulkInsert.Perform();
            //FormMain.MoveTo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string gw = Path.GetFileName(Path.GetDirectoryName("E:\\SecuredFTP\\Test"));
            //string[] dirs = Directory.GetDirectories(@"E:\SecuredFTP\Test");
            //foreach (var d in dirs)
            //{
            //    Console.WriteLine(d);
            //}

            //string[] dirs = Directory.GetFiles(@"E:\SecuredFTP\Test", "*.csv", SearchOption.AllDirectories);
            //foreach (var file in dirs)
            //{
            //    Console.WriteLine(file);
            //    string[] allLines = File.ReadAllLines(file);
            //    var columnCount = allLines[0].Split(',').Length;
            //    if (columnCount == 3)
            //    {
            //        Task.Factory.StartNew(() => BulkOMS.Import(file));
            //    }
            //    else if (columnCount == 11)
            //    {
            //        Task.Factory.StartNew(() => BulkRDS.Import(file));
            //    }
            //}

            //Directory.MoveTo(@"E:\Test", @"E:\BackupFTP");

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(@"E:\SecuredFTP\Test", "*.*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(@"E:\SecuredFTP\Test", @"C:\SecuredBackup"));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(@"E:\SecuredFTP\Test", "*.csv",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(@"E:\SecuredFTP\Test", @"C:\SecuredBackup"), true);

        }
    }
}
