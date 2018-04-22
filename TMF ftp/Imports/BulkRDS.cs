using System;
using System.Data;
using System.Data.SqlClient;
using TMF_ftp.Core;

namespace TMF_ftp.Imports
{
    public class BulkRDS
    {
        private static string _gw;
        private static int _final;

        public BulkRDS()
        {

        }
        public static void Import(string ofdFilename)
        {
            int count = 0;

            using (SqlConnection conn = new SqlConnection(new SmartDB().Connection.ConnectionString))
            {
                conn.Open();

                DataTable newMeter = MakeTable.RDS(ofdFilename);

                InsertMeterBulkCopy(conn, newMeter);
            }
        }

        private static void InsertMeterBulkCopy(SqlConnection connection, DataTable dtRDS)
        {
            using (SqlBulkCopy s = new SqlBulkCopy(connection))
            {
                s.DestinationTableName = "tblRDS";
                s.ColumnMappings.Add("Id", "Id");
                s.ColumnMappings.Add("METER_ADDRESS", "METER_ADDRESS");
                s.ColumnMappings.Add("READING_DATE", "READING_DATE");
                s.ColumnMappings.Add("READING_VALUE_L", "READING_VALUE_L");
                s.ColumnMappings.Add("LOW_BATTERY_ALR", "LOW_BATTERY_ALR");
                s.ColumnMappings.Add("LEAK_ALR", "LEAK_ALR");
                s.ColumnMappings.Add("MAGNETIC_TAMPER_ALR", "MAGNETIC_TAMPER_ALR");
                s.ColumnMappings.Add("METER_ERROR_ALR", "METER_ERROR_ALR");
                s.ColumnMappings.Add("BACK_FLOW_ALR", "BACK_FLOW_ALR");
                s.ColumnMappings.Add("BROKEN_PIPE_ALR", "BROKEN_PIPE_ALR");
                s.ColumnMappings.Add("EMPTY_PIPE_ALR", "EMPTY_PIPE_ALR");
                s.ColumnMappings.Add("SPECIFIC_ERROR_ALR", "SPECIFIC_ERROR_ALR");
                s.ColumnMappings.Add("MAC_ADDRESS", "MAC_ADDRESS");

                try
                {
                    s.WriteToServer(dtRDS);
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
