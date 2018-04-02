using Standard.Licensing;
using Standard.Licensing.Validation;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TMF_ftp.TMFLicensing
{
    public delegate void ShowFrm();
    public partial class Register : Form
    {
        public event ShowFrm evtFrm;
        public Register()
        {
            InitializeComponent();
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            var path = textBoxLicense.Text;
            var publicKey = textBoxPublicKey.Text;
            using (var sr = new StreamReader(path))
            {
                var license = License.Load(sr);

                var validationFailures = license.Validate()
                    .ExpirationDate()
                    .When(lic => lic.Type == LicenseType.Trial)
                    .And()
                    .Signature(publicKey)
                    .AssertValidLicense();

                if (validationFailures.Any())
                {
                    MessageBox.Show("Invalid License!");
                }
                else
                {
                    using (StreamWriter writer =
                        new StreamWriter("license.txt"))
                    {
                        writer.Write(textBoxPublicKey.Text);
                    }
                    File.Copy(textBoxLicense.Text, Application.StartupPath + "\\License.lic", true);
                    evtFrm?.Invoke();
                    MessageBox.Show("Valid License! Thank you.");
                }
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "License file|*.lic";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxLicense.Text = openFileDialog.FileName;
            }
        }
    }
}
