using System;
using System.Data;
using System.Data.SqlClient;
using TMF_ftp.Core;

namespace TMF_ftp.Imports
{
    public class BulkOMS
    {
        public BulkOMS()
        {

        }
        public void Import(string ofdFilename)
        {
            int count = 0;

            using (SqlConnection conn = new SqlConnection(new SmartDB().Connection.ConnectionString))
            {
                conn.Open();

                DataTable newMeter = new MakeTable().OMS(ofdFilename);

                InsertMeterBulkCopy(conn, newMeter);
            }
        }

        private void InsertMeterBulkCopy(SqlConnection conn, DataTable dtOMS)
        {
            using (SqlBulkCopy s = new SqlBulkCopy(conn))
            {
                s.DestinationTableName = "tblOMS";
                s.ColumnMappings.Add("Id", "Id");
                s.ColumnMappings.Add("METER_ADDRESS", "METER_ADDRESS");
                s.ColumnMappings.Add("READING_DATE", "READING_DATE");
                s.ColumnMappings.Add("PACKET", "PACKET");
                s.ColumnMappings.Add("MAC_ADDRESS", "MAC_ADDRESS");

                try
                {
                    s.WriteToServer(dtOMS);
                    Console.WriteLine($"Importing was successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Contact Admin: {ex.Message}", "Import");
                }
            }
        }
    }
}
