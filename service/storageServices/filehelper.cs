using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StorageServices
{
    public static class FileHelper
    {
        public static void MakeDirIfNotExists(string input)
        {
            string dirname = "";
            string[] split = input.Split('\\');

            var i = 0;
            foreach (var splititem in split)
            {
                if (i != 0)
                {
                    dirname += "\\";
                }
                dirname += splititem;
                if (!Directory.Exists(dirname))
                {
                    Directory.CreateDirectory(dirname);
                }
                i++;
            }
        }


        public static void SaveFileData(byte[] data,string folder,  string filename ){
            MakeDirIfNotExists(folder);
            File.WriteAllBytes(folder + "/" + filename, data);
        }

        public static byte[] GetFileData(string filename)
        {
            return File.ReadAllBytes(filename);
        }

        public static byte[] Serialize(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public static Object Unserialize(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }


        internal static string[] GetAllFolders(string folder)
        {
            MakeDirIfNotExists(folder);
            return Directory.GetDirectories(folder).Select(x=>x.Split('\\').Last()).ToArray();
            
        }
    }
}
