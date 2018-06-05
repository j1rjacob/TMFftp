using System;
using System.IO;

namespace TMF_ftp.Services
{
    public class Backup
    {
        public Backup()
        {
        }

        public void MoveTo(string sourcePath)
        {
            try
            {
                CreateBackupDirectory(sourcePath);
                CopyAllAndReplaceFilesWithSameName(sourcePath);
                DeleteFile(sourcePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void DeleteFile(string sourcePath)
        {
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Delete(newPath);
        }

        private static void CopyAllAndReplaceFilesWithSameName(string sourcePath)
        {
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.csv",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, @"C:\SecuredBackup"), true);
            Console.WriteLine("Moving to backup is successful.");
        }

        private static void CreateBackupDirectory(string sourcePath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*.*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, @"C:\SecuredBackup"));
        }
    }
}