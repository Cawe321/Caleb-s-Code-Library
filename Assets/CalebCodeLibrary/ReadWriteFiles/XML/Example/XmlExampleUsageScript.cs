using CalebCodeLibrary.ReadWriteFiles.XML;
using UnityEngine;

namespace CalebCodeLibrary.ReadWriteFiles.XML.Example
{
    public class XmlExampleUsageScript : MonoBehaviour
    {
        [SerializeField]
        XmlExampleDummyObject dummyObject;

        [SerializeField]
        string filePath = "ProjectData/XML_Files";

        [SerializeField]
        string fileName = "XmlExampleDummyObject.xml";

        [SerializeField]
        bool usePersistentDataPath = true;
        public void Write()
        {
            /* Quick Method of initializing XmlLoader*/
            XmlLoader<XmlExampleDummyObject>.QuickInitialize(filePath, fileName, usePersistentDataPath);

            /* Manual Method of intializing XmlLoader  
            XmlLoader<XmlExampleDummyObject>.FilePath = filePath;
            XmlLoader<XmlExampleDummyObject>.FileName = fileName;
            XmlLoader<XmlExampleDummyObject>.UsePersistentDataPath = usePersistentDataPath;
            */

            // We set this to true to so that it will print relevant text to console. Default is false.
            XmlLoader<XmlExampleDummyObject>.DebugMode = true; // This line is not neccessary

            // Write this object to xml file
            XmlLoader<XmlExampleDummyObject>.WriteToFile(dummyObject);
        }

        public void Read()
        {
            /* Quick Method of initializing XmlLoader*/
            XmlLoader<XmlExampleDummyObject>.QuickInitialize(filePath, fileName, usePersistentDataPath);

            /* Manual Method of intializing XmlLoader  
            XmlLoader<XmlExampleDummyObject>.FilePath = filePath;
            XmlLoader<XmlExampleDummyObject>.FileName = fileName;
            XmlLoader<XmlExampleDummyObject>.UsePersistentDataPath = usePersistentDataPath;
            */

            // We set this to true to so that it will print relevant text to console. Default is false.
            XmlLoader<XmlExampleDummyObject>.DebugMode = true; // This line is not neccessary

            XmlExampleDummyObject readObject = XmlLoader<XmlExampleDummyObject>.ReadFromFile();
            if (readObject != null) // Check if the value is null. This may happen when the file is not found.
                readObject.PrintValuesToConsole(); // Print the values to console
        }
    }
}
