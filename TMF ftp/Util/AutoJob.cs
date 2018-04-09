using Quartz;
using System;

namespace TMF_ftp.Util
{

    public class AutoJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                FormMain.PerformDownload();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
