using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewroke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Proga";
            FileHelper.DirectoryExists(path);
            FileHelper.FileExists(path + @"\place.log");
            FileHelper.FileExists(path + @"\taken.log");
            FileHelper.FileExists(path + @"\moved.log");
            FileHelper.FileExists(path + @"\failed.log");

            Shelf shelfA = new Shelf();
            Shelf shelfB = new Shelf();


        }
    }
}
