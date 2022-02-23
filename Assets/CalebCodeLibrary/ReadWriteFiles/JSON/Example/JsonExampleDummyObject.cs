using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalebCodeLibrary.ReadWriteFiles.Json.Example
{
    [System.Serializable]
    public class JsonExampleDummyObject
    {
        public int integer = 5;
        public bool boolean = true;
        public Vector3 vector3 = new Vector3(5, 5, 5);

        public void PrintValuesToConsole()
        {
            Debug.Log($"Integer = {integer}, Boolean = {boolean}, Vector3 = {vector3}");
        }
    }
}