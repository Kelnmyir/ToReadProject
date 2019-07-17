using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FunWithIO
{
    class Program
    {
        static void Main()
        {
            string path = Console.ReadLine();
            if (Directory.Exists(path))
            {
                DirectoryInfo dirToPrint = new DirectoryInfo(path);
                GetDirsAndFiles(dirToPrint);
            }
            else
                Console.WriteLine("Invalid path");
            Console.ReadLine();
        }
        public static void GetDirsAndFiles (DirectoryInfo mainDir)
        {
            DirectoryInfo[] dirs = mainDir.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                Console.WriteLine();
                Console.WriteLine(dir.FullName);
                Console.WriteLine("contains files:");
                GetDirsAndFiles(dir);
            }
            FileInfo[] files = mainDir.GetFiles("*.*");
            foreach (FileInfo file in files)
                Console.WriteLine(file.Name);
        }
    }
}
