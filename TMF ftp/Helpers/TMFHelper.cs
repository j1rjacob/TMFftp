using System;
using System.IO;

namespace TMF_ftp.Helpers
{
    public static class TMFHelper
    {
        public static bool ChkIsDirectory(this string file)
        {
            FileAttributes attr = File.GetAttributes(file);

            Console.WriteLine($"The path of attr: {attr}");

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return true;
            else
                return false;
        }
        public static bool ChkIsSymbolicLink(this string file)
        {
            FileInfo pathInfo = new FileInfo(file);
            return pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
        }
    }
}
