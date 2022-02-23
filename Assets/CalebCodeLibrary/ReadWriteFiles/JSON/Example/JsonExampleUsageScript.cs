using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalebCodeLibrary.ReadWriteFiles.Json.Example
{
    public class JsonExampleUsageScript : MonoBehaviour
    {
        [SerializeField]
        JsonExampleDummyObject dummyObject;

        [SerializeField]
        string filePath = "ProjectData/Json_Files";

        [SerializeField]
        string fileName = "JsonExampleDummyObject.json";

        [SerializeField]
        bool usePersistentDataPath = true;
        public void Write()
        {
            /* Quick Method of initializing JsonLoader*/
            JsonLoader<JsonExampleDummyObject>.QuickInitialize(filePath, fileName, usePersistentDataPath);

            /* Manual Method of intializing JsonLoader  
            JsonLoader<JsonExampleDummyObject>.FilePath = filePath;
            JsonLoader<JsonExampleDummyObject>.FileName = fileName;
            JsonLoader<JsonExampleDummyObject>.UsePersistentDataPath = usePersistentDataPath;
            */

            // We set this to true to so that it will print relevant text to console. Default is false.
            JsonLoader<JsonExampleDummyObject>.DebugMode = true; // This line is not neccessary

            // Write this object to json file
            JsonLoader<JsonExampleDummyObject>.WriteToFile(dummyObject);
        }

        public void Read()
        {
            /* Quick Method of initializing JsonLoader*/
            JsonLoader<JsonExampleDummyObject>.QuickInitialize(filePath, fileName, usePersistentDataPath);

            /* Manual Method of intializing JsonLoader  
            JsonLoader<JsonExampleDummyObject>.FilePath = filePath;
            JsonLoader<JsonExampleDummyObject>.FileName = fileName;
            JsonLoader<JsonExampleDummyObject>.UsePersistentDataPath = usePersistentDataPath;
            */

            // We set this to true to so that it will print relevant text to console. Default is false.
            JsonLoader<JsonExampleDummyObject>.DebugMode = true; // This line is not neccessary

            JsonExampleDummyObject readObject = JsonLoader<JsonExampleDummyObject>.ReadFromFile();
            if (readObject != null) // Check if the value is null. This may happen when the file is not found.
                readObject.PrintValuesToConsole(); // Print the values to console
        }
    }
}
