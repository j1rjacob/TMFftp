using Quartz;
using System;

namespace TMF_ftp.Util
{
    public delegate void MainFrm();
    public class AutoJob : IJob
    {
        public event MainFrm evtFrm;
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                FormMain.PerformDownload();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
