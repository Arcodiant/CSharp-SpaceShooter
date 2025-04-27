using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Runtime.InteropServices;

namespace Space_Shooter
{
    static class SaveData
    {
        public static void CreateSave(string path)
        {
            File.Create(path);
        }

        public static void DeleteSave(string path)
        {
            File.Delete(path);
        }

        public static void SelectSave(string path)
        {

        }

        public static void ReadSave(string path)
        {

        }
    }
}
