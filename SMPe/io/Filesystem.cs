using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SMPe.Properties;
using SMPe.bea;

namespace SMPe.io
{
    class Filesystem
    {
        public static bool DoesFolderExist(string folder)
        {
            return Directory.Exists(Settings.Default.folderPath + folder);
        }

        public static bool DoesFileExist(string fileName)
        {
            return File.Exists(Settings.Default.folderPath + fileName);
        }

        public static void MakeDirectory(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }

        public static BEA GetArchive(string fileName)
        {
            if (!DoesFileExist(fileName))
                return null;

            EndianBinaryReader reader = new EndianBinaryReader(File.Open(fileName, FileMode.Open));

            return new BEA(ref reader);
        }
    }
}
