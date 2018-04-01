using Standard.Licensing;
using Standard.Licensing.Validation;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TMF_ftp.TMFLicensing
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "License file|*.lic";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxLicense.Text = openFileDialog.FileName;
            }
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
                    //Store to Db
                    using (StreamWriter writer =
                        new StreamWriter("license.txt"))
                    {
                        writer.Write(textBoxPublicKey.Text);
                    }
                    File.Copy(textBoxLicense.Text, Application.StartupPath + "License.lic", true);
                    MessageBox.Show("Valid License!");
                }
            }
        }
    }
}
