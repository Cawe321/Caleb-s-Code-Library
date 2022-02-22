using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace CalebCodeLibrary.ReadWriteFiles.XML
{
    /// <summary>
    /// A static class that can read/write XML files using <see cref="System.Xml.Serialization.XmlSerializer"/> where T is a System.Serializable class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class XmlLoader<T> where T : class
    {
        /// <summary>
        /// The file path where the loader should read/write.
        /// Default: "ProjectData/XML_Files"
        /// </summary>
        public static string FilePath = "ProjectData/XML_Files";
        /// <summary>
        /// The file name of the XML file.
        /// Default: "XML_Object.xml"
        /// </summary>
        public static string FileName = "XML_Object.xml";

        /// <summary>
        /// Whether to append <see cref="FileName"/> after <see cref="Application.persistentDataPath"/> when reading/writing files.
        /// </summary>
        public static bool UsePersistentDataPath = true;

        /// <summary>
        /// Whether to print relevant text to console
        /// </summary>
        public static bool DebugMode = false;

        /// <summary>
        /// Writes to file using <see cref="FilePath"/>, <see cref="FileName"/> and <see cref="UsePersistentDataPath"/> values.
        /// </summary>
        /// <param name="xmlObject">The xml object that we are writing about to the file.</param>    
        public static void WriteToFile(T xmlObject)
        {
            // Define the full file path 
            string finalFilePath = GetFilePath();

            string fileDirectory = "";
            if (UsePersistentDataPath)
                fileDirectory = Application.persistentDataPath;
            fileDirectory = Path.Combine(fileDirectory, FilePath);
            Directory.CreateDirectory(fileDirectory);

            // Prepare to write to XML file
            XmlSerializer serializer = new XmlSerializer(xmlObject.GetType());
            FileStream fileStream = new FileStream(finalFilePath, FileMode.Create); // FileMode.Create is used as it will overwrite the file if it exists already
            serializer.Serialize(fileStream, xmlObject);
            fileStream.Close(); // End of writing to XML File

            if (DebugMode)
                Debug.Log("XmlLoader: File has written to:\n" + Path.GetFullPath(finalFilePath));
        }

        /// <summary>
        /// Reads from file using <see cref="FilePath"/>, <see cref="FileName"/> and <see cref="UsePersistentDataPath"/> values. 
        /// </summary>
        /// <returns>T value read from file. Returns null if file is not found.</returns>
        public static T ReadFromFile()
        {
            // Define the full file path 
            string finalFilePath = GetFilePath();

            if (!File.Exists(finalFilePath))
            {
                Debug.LogWarning("XmlLoader: ReadFromFile() was called but the file was not found!");
                return null;
            }
            if (DebugMode)
                Debug.Log("XmlLoader: File has been found!");

            // Prepare to read from XML file
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream fileStream = new FileStream(finalFilePath, FileMode.Open); // FileMode.Create is used as it will overwrite the file if it exists already
            T xmlObject = serializer.Deserialize(fileStream) as T;
            fileStream.Close(); // End of reading from XML File

            return xmlObject;
        }

        /// <summary>
        /// One function to initialize important values. See <see cref="FilePath"/>, <see cref="FileName"/> and <see cref="UsePersistentDataPath"/> about the parameters.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="usePersistentDataPath"></param>
        public static void QuickInitialize(string filePath, string fileName, bool usePersistentDataPath)
        {
            FilePath = filePath;
            FileName = fileName;
            UsePersistentDataPath = usePersistentDataPath;
        }

        static string GetFilePath()
        {
            string finalFilePath = Path.Combine(FilePath, FileName);
            if (UsePersistentDataPath) // if we are using persistent data path
                finalFilePath = Path.Combine(Application.persistentDataPath, finalFilePath);
            return finalFilePath;
        }
    }
}
