using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using TMF_ftp.Core;

namespace TMF_ftp.Imports
{
    public static class FetchTable
    {   //TODO Check when no data on the table
        public static DataTable GetRDS()
        {
            using (SqlConnection conn = new SqlConnection(new SmartDB().Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblRDS", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        ds.Locale = CultureInfo.InvariantCulture;
                        da.Fill(ds, "tblRDS");

                        DataTable rds = ds.Tables["tblRDS"];

                        IEnumerable<DataRow> query = from g in rds.AsEnumerable()
                                                     select g;

                        DataTable boundTable = query.CopyToDataTable<DataRow>();
                        return boundTable;
                    }
                }
            }
        }
        public static DataTable GetOMS()
        {
            using (SqlConnection conn = new SqlConnection(new SmartDB().Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblOMS", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        ds.Locale = CultureInfo.InvariantCulture;
                        da.Fill(ds, "tblOMS");

                        DataTable meters = ds.Tables["tblOMS"];

                        IEnumerable<DataRow> query =
                            from m in meters.AsEnumerable()
                            select m;

                        DataTable boundTable = query.CopyToDataTable<DataRow>();
                        return boundTable;
                    }
                }
            }
        }
    }
}
