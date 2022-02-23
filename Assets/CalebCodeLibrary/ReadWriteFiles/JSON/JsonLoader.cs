using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace CalebCodeLibrary.ReadWriteFiles.Json
{
    public static class JsonLoader<T> where T : class
    {
        /// <summary>
        /// The file path where the loader should read/write.
        /// Default: "ProjectData/Json_Files"
        /// </summary>
        public static string FilePath = "ProjectData/Json_Files";
        /// <summary>
        /// The file name of the Json file.
        /// Default: "Json_Object.json"
        /// </summary>
        public static string FileName = "Json_Object.json";

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
        /// <param name="jsonObject">The json object that we are writing about to the file.</param>    
        public static void WriteToFile(T jsonObject)
        {
            // Define the full file path 
            string finalFilePath = GetFilePath();

            string fileDirectory = "";
            if (UsePersistentDataPath)
                fileDirectory = Application.persistentDataPath;
            fileDirectory = Path.Combine(fileDirectory, FilePath);
            Directory.CreateDirectory(fileDirectory);

            // Prepare to write to Json file
            string jsonString = JsonUtility.ToJson(jsonObject, true);
            File.WriteAllText(finalFilePath, jsonString);
            // End of writing to Json File

            if (DebugMode)
                Debug.Log("JsonLoader: File has written to:\n" + finalFilePath);
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
                Debug.LogWarning("JsonLoader: ReadFromFile() was called but the file was not found!");
                return null;
            }
            if (DebugMode)
                Debug.Log("JsonLoader: File has been found!");

            // Prepare to read from Json file
            string jsonString = File.ReadAllText(finalFilePath);
            T jsonObject = (T)JsonUtility.FromJson(jsonString, typeof(T));
            FileStream fileStream = new FileStream(finalFilePath, FileMode.Open); // FileMode.Create is used as it will overwrite the file if it exists already
            fileStream.Close(); // End of reading from Json File

            return jsonObject;
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
            return Path.GetFullPath(finalFilePath);
        }
    }
}
