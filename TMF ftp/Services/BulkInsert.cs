using System;
using System.IO;
using TMF_ftp.Imports;

namespace TMF_ftp.Services
{
    public class BulkInsert
    {
        public BulkInsert()
        {
        }

        public void Perform(string sourcePath)
        {
            try
            {
                Console.WriteLine("Process Bulk Insert.");
                string[] dirs = Directory.GetFiles(sourcePath, "*.csv", SearchOption.AllDirectories);
                foreach (var file in dirs)
                {
                    string[] allLines = File.ReadAllLines(file);
                    var columnCount = allLines[0].Split(',').Length;
                    if (columnCount == 3)
                    {
                        new BulkOMS().Import(file);
                    }
                    else if (columnCount == 11)
                    {
                        new BulkRDS().Import(file);
                    }
                    Console.WriteLine("Finish Bulk Insert.");
                    new Backup().MoveTo(sourcePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}