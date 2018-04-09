using QLicense;
using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Windows.Forms;
using TMFLicense;

namespace TMFActivitionTool
{
    public partial class frmMain : Form
    {
        private byte[] _certPubicKeyData;
        private SecureString _certPwd = new SecureString();
        public frmMain()
        {
            InitializeComponent();

            _certPwd.AppendChar('a');
            _certPwd.AppendChar('j');
            _certPwd.AppendChar('f');
            _certPwd.AppendChar('f');
            _certPwd.AppendChar('J');
            _certPwd.AppendChar('N');
            _certPwd.AppendChar('R');
            _certPwd.AppendChar('X');
            _certPwd.AppendChar('1');
            _certPwd.AppendChar('4');
            _certPwd.AppendChar('3');
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Read public key from assembly
            Assembly _assembly = Assembly.GetExecutingAssembly();
            using (MemoryStream _mem = new MemoryStream())
            {
                _assembly.GetManifestResourceStream("TMFActivitionTool.LicenseSign.pfx").CopyTo(_mem);

                _certPubicKeyData = _mem.ToArray();
            }

            //Initialize the path for the certificate to sign the XML license file
            licSettings.CertificatePrivateKeyData = _certPubicKeyData;
            licSettings.CertificatePassword = _certPwd;

            //Initialize a new license object
            licSettings.License = new MyLicense();
        }

        private void licSettings_OnLicenseGenerated(object sender, QLicense.Windows.Controls.LicenseGeneratedEventArgs e)
        {
            //Event raised when license string is generated. Just show it in the text box
            licString.LicenseString = e.LicenseBASE64String;
        }

        private void btnGenSvrMgmLic_Click(object sender, EventArgs e)
        {
            //Event raised when "Generate License" button is clicked. 
            //Call the core library to generate the license
            licString.LicenseString = LicenseHandler.GenerateLicenseBASE64String(
                new MyLicense(),
                _certPubicKeyData,
                _certPwd);
        }
    }
}
