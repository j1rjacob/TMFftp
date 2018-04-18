using System;
using System.IO;
using System.Text;

namespace TMF_ftp.Helpers
{
    public class ConsoleWriter : TextWriter
    {
        private System.Windows.Forms.RichTextBox rTextBox;
        public ConsoleWriter(System.Windows.Forms.RichTextBox textBox)
        {
            this.rTextBox = textBox;
        }

        public override void WriteLine(string str)
        {
            //this.rTextBox.AppendText(str + "\n");
            try
            {
                this.rTextBox.Invoke((Action) delegate { this.rTextBox.AppendText(str + "\n"); });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //Environment.Exit(0);
                throw;
                //Application.Exit();
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
