using System;
using System.IO;

namespace NiceShot.Core.Helper
{
    public class IOHelper
    {

        public static void CreateFileInFilesDir(string fileName, string content)
        {
            CreateCustomFile("files", fileName, content);
        }

        private static void CreateCustomFile(string dirName,string fileName,string content)
        {
            var fullDirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,dirName);
            if (!Directory.Exists(fullDirName))
                Directory.CreateDirectory(fullDirName);

            var fullFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dirName, fileName);
            if (File.Exists(fullFileName))
                File.WriteAllText(fullFileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fullFileName))
            {
                sw.WriteLine(content);
            }
        }

    }
}
