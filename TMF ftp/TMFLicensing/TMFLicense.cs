using Standard.Licensing;
using Standard.Licensing.Validation;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TMF_ftp.TMFLicensing
{
    public static class TMFLicense
    {
        public static bool Validate()
        {
            try
            {
                var path = Application.StartupPath + "\\License.lic";
                var publicKey = File.ReadAllText(Application.StartupPath + "\\license.txt");
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
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
