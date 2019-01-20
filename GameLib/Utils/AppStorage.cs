using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace GameLib
{
    public class AppStorage
    {
        private readonly string appDir;

        public AppStorage(string dir)
        {
            appDir = dir;

            if (!Directory.Exists(GetAppPath())) {
                Directory.CreateDirectory(GetAppPath());
            }
        }

        private string GetExt()
        {
            return ".dat";
        }

        private string GetAppPath()
        {
            return Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments),
                appDir
            );
        }

        private string GetFilePath(string name)
        {
            return Path.Combine(
                GetAppPath(),
                name + GetExt()
            );
        }

        public void Save<T>(T objectToSave, string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(GetFilePath(fileName), FileMode.Create, FileAccess.Write);
            bf.Serialize(fs, objectToSave);
            fs.Close();
        }

        public T Load<T>(string filePath)
        {
            using (Stream stream = File.Open(GetFilePath(filePath), FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();

                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public bool Exists(string name) => File.Exists(GetFilePath(name));

        public void Clear(string name) 
        {
            if (Exists(name))
            {
                File.Delete(GetFilePath(name));
            }   
        }

        public void ClearAll() 
        { 
            foreach(var fileName in Files())
            {
                Clear(fileName);
            }
        }

        public string[] Files()
        {
            return Directory.GetFiles(GetAppPath(), $"*{GetExt()}")
                            .Select(Path.GetFileNameWithoutExtension)
                            .ToArray();
        }
    }

}
