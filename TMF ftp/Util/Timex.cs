using System;

namespace TMF_ftp.Util
{
    public class Timex
    {
        public string DBtoCSVDateConvert(string DBDate)
        {
            if (DBDate == null)
            {
                Console.WriteLine(DBDate);
                return "";
            }
            var dbDate = DBDate.Split(null);
            var datex = DateTime.Parse(dbDate[0]);
            var time24 = DateTime.Parse(dbDate[1] + " " + dbDate[2]);
            return time24.ToString("HH:mm:ss") + " " + datex.ToString("dd/MM/yyyy");
        }
        public DateTime CSVtoDateDateConvert(string CSVDate)
        {
            var dbDate = CSVDate.Split(null);
            var datex = dbDate[1].Split('/');
            return Convert.ToDateTime(datex[1] + "/" + datex[0] + "/" + datex[2] + " " + dbDate[0]);
        }
    }
}
