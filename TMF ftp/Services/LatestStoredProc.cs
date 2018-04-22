using System;
using System.Data;
using System.Data.SqlClient;
using TMF_ftp.Core;

namespace TMF_ftp.Services
{
    internal static class LatestStoredProc
    {
        public static void UpdateOMSRDSLatest()
        {
            Console.WriteLine("Start updating database");
            try
            {
                string[] storeProc = new[] {"LATEST_OMS_READING", "LATEST_RDS_READING"};

                foreach (var sp in storeProc)
                {
                    using (var conn = new SqlConnection(new SmartDB().Connection.ConnectionString))
                    using (var command = new SqlCommand(sp, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("Finish updating database");
        }
    }
}