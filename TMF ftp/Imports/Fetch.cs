using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using TMF_ftp.Core;

namespace TMF_ftp.Imports
{
    public class Fetch
    {   //TODO Check when no data on the table
        public DataTable TableRDS()
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

        public DataTable TableOMS()
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
