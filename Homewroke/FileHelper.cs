using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewroke
{
    internal class FileHelper
    {
        public static void DirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void FileExists(string path)
        {
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
            }
        }

        public static string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }

        public static void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
