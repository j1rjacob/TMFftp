using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TMF_ftp.Imports
{
    public static class MakeTable
    {
        public static DataTable RDS(string Filename)
        {
            string gw = Path.GetFileName(Path.GetDirectoryName(Filename));

            DataTable newMeterReading = new DataTable("tblRDS");

            DataColumn meterReadingId = new DataColumn();
            meterReadingId.DataType = Type.GetType("System.String");
            meterReadingId.ColumnName = "Id";
            newMeterReading.Columns.Add(meterReadingId);

            DataColumn meterAddress = new DataColumn();
            meterAddress.DataType = Type.GetType("System.String");
            meterAddress.ColumnName = "METER_ADDRESS";
            newMeterReading.Columns.Add(meterAddress);

            DataColumn readingDate = new DataColumn();
            readingDate.DataType = Type.GetType("System.DateTime");
            readingDate.ColumnName = "READING_DATE";
            newMeterReading.Columns.Add(readingDate);

            DataColumn readingValue = new DataColumn();
            readingValue.DataType = Type.GetType("System.String");
            readingValue.ColumnName = "READING_VALUE_L";
            newMeterReading.Columns.Add(readingValue);

            DataColumn lowBattery = new DataColumn();
            lowBattery.DataType = Type.GetType("System.String");
            lowBattery.ColumnName = "LOW_BATTERY_ALR";
            newMeterReading.Columns.Add(lowBattery);

            DataColumn leakAlr = new DataColumn();
            leakAlr.DataType = Type.GetType("System.String");
            leakAlr.ColumnName = "LEAK_ALR";
            newMeterReading.Columns.Add(leakAlr);

            DataColumn magneticTamper = new DataColumn();
            magneticTamper.DataType = Type.GetType("System.String");
            magneticTamper.ColumnName = "MAGNETIC_TAMPER_ALR";
            newMeterReading.Columns.Add(magneticTamper);

            DataColumn meterError = new DataColumn();
            meterError.DataType = Type.GetType("System.String");
            meterError.ColumnName = "METER_ERROR_ALR";
            newMeterReading.Columns.Add(meterError);

            DataColumn backFlow = new DataColumn();
            backFlow.DataType = Type.GetType("System.String");
            backFlow.ColumnName = "BACK_FLOW_ALR";
            newMeterReading.Columns.Add(backFlow);

            DataColumn brokenPipe = new DataColumn();
            brokenPipe.DataType = Type.GetType("System.String");
            brokenPipe.ColumnName = "BROKEN_PIPE_ALR";
            newMeterReading.Columns.Add(brokenPipe);

            DataColumn emptyPipe = new DataColumn();
            emptyPipe.DataType = System.Type.GetType("System.String");
            emptyPipe.ColumnName = "EMPTY_PIPE_ALR";
            newMeterReading.Columns.Add(emptyPipe);

            DataColumn specificErr = new DataColumn();
            specificErr.DataType = Type.GetType("System.String");
            specificErr.ColumnName = "SPECIFIC_ERROR_ALR";
            newMeterReading.Columns.Add(specificErr);

            DataColumn macAddress = new DataColumn();
            macAddress.DataType = Type.GetType("System.String");
            macAddress.ColumnName = "MAC_ADDRESS";
            newMeterReading.Columns.Add(macAddress);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = meterReadingId;
            newMeterReading.PrimaryKey = keys;

            try
            {
                string[] allLines = File.ReadAllLines(Filename);
                var columnCount = allLines[0].Split(',').Length;
                //if (columnCount == 11)
                //{
                    var query = from line in allLines
                                let data = line.Split(',')
                                select new
                                {
                                    METER_ADDRESS = data[0],
                                    READING_DATE = data[1],
                                    READING_VALUE_L = data[2],
                                    LOW_BATTERY_ALR = data[3],
                                    LEAK_ALR = data[4],
                                    MAGNETIC_TAMPER_ALR = data[5],
                                    METER_ERROR_ALR = data[6],
                                    BACK_FLOW_ALR = data[7],
                                    BROKEN_PIPE_ALR = data[8],
                                    EMPTY_PIPE_ALR = data[9],
                                    SPECIFIC_ERROR_ALR = data[10]
                                };
                    DataRow row;
                    foreach (var q in query.ToList().Skip(1))
                    {
                        row = newMeterReading.NewRow();
                        row["Id"] = Guid.NewGuid().ToString();
                        row["METER_ADDRESS"] = q.METER_ADDRESS.Replace("-", "");
                        row["READING_DATE"] = DateTime.ParseExact(q.READING_DATE, "HH:mm:ss dd/MM/yyyy", new CultureInfo("en-US"));
                        row["READING_VALUE_L"] = q.READING_VALUE_L;
                        row["LOW_BATTERY_ALR"] = q.LOW_BATTERY_ALR;
                        row["LEAK_ALR"] = q.LEAK_ALR;
                        row["MAGNETIC_TAMPER_ALR"] = q.MAGNETIC_TAMPER_ALR;
                        row["METER_ERROR_ALR"] = q.METER_ERROR_ALR;
                        row["BACK_FLOW_ALR"] = q.BACK_FLOW_ALR;
                        row["BROKEN_PIPE_ALR"] = q.BROKEN_PIPE_ALR;
                        row["EMPTY_PIPE_ALR"] = q.EMPTY_PIPE_ALR;
                        row["SPECIFIC_ERROR_ALR"] = q.SPECIFIC_ERROR_ALR;
                        //row["MAC_ADDRESS"] = Regex.Replace(gw, @"^(..)(..)(..)(..)(..)(..)$", "$1:$2:$3:$4:$5:$6");
                        row["MAC_ADDRESS"] = gw;
                        newMeterReading.Rows.Add(row);
                    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Contact Admin: {ex.Message}", "Import");
            }

            newMeterReading.AcceptChanges();

            return newMeterReading;
        }
        public static DataTable OMS(string Filename)
        {
            string gw = Path.GetFileName(Path.GetDirectoryName(Filename));
            DataTable newMeterReading = new DataTable("tblOMS");

            DataColumn meterReadingId = new DataColumn();
            meterReadingId.DataType = Type.GetType("System.String");
            meterReadingId.ColumnName = "Id";
            newMeterReading.Columns.Add(meterReadingId);

            DataColumn meterAddress = new DataColumn();
            meterAddress.DataType = Type.GetType("System.String");
            meterAddress.ColumnName = "METER_ADDRESS";
            newMeterReading.Columns.Add(meterAddress);

            DataColumn readingDate = new DataColumn();
            readingDate.DataType = Type.GetType("System.DateTime");
            readingDate.ColumnName = "READING_DATE";
            newMeterReading.Columns.Add(readingDate);

            DataColumn packet = new DataColumn();
            packet.DataType = Type.GetType("System.String");
            packet.ColumnName = "PACKET";
            newMeterReading.Columns.Add(packet);

            DataColumn macAddress = new DataColumn();
            macAddress.DataType = Type.GetType("System.String");
            macAddress.ColumnName = "MAC_ADDRESS";
            newMeterReading.Columns.Add(macAddress);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = meterReadingId;
            newMeterReading.PrimaryKey = keys;

            try
            {
                string[] allLines = File.ReadAllLines(Filename);
                var columnCount = allLines[0].Split(',').Length;
                //if (columnCount == 3)
                //{   //OMS
                    var query = from line in allLines
                                let data = line.Split(',')
                                select new
                                {
                                    METER_ADDRESS = data[0],
                                    READING_DATE = data[1],
                                    PACKET = data[2]
                                };
                    DataRow row;
                    foreach (var q in query.ToList().Skip(1))
                    {
                        row = newMeterReading.NewRow();
                        row["Id"] = Guid.NewGuid().ToString(); 
                        row["METER_ADDRESS"] = q.METER_ADDRESS.Replace("-", "");
                        row["READING_DATE"] = DateTime.ParseExact(q.READING_DATE, "HH:mm:ss dd/MM/yyyy", new CultureInfo("en-US"));
                        row["PACKET"] = q.PACKET;
                        //row["MAC_ADDRESS"] = Regex.Replace(gw, @"^(..)(..)(..)(..)(..)(..)$", "$1:$2:$3:$4:$5:$6");
                        row["MAC_ADDRESS"] = gw;
                        newMeterReading.Rows.Add(row);
                    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Contact Admin: {ex.Message}", "Import");
            }

            newMeterReading.AcceptChanges();

            return newMeterReading;
        }
    }
}
