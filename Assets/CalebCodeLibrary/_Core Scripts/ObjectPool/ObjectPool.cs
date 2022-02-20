using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalebCodeLibrary.ObjectPool
{
    /// <summary>
    /// A script that helps to identify an object pool(The parent of all objects that are in the pool).
    /// T is the class type the object pool should return.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObjectPool : MonoBehaviour
    {
        #region SETTING_VARIABLES
        [Header("Settings")]
        [Tooltip("[OPTIONAL] Used to help identify the type of object pool this is.")]
        public string objectType;

        [Tooltip("The object that the script will create and add into the pool whenever requested.")]
        [SerializeField]
        GameObject originalObject;

        [Tooltip("The number of copies of the original object the script will create at start.")]
        [SerializeField]
        int defaultNumberOfObjects;

        #endregion

        /// <summary>
        /// Call this to Instantiate given <see cref="ObjectPool.originalObject"/> by <see cref="ObjectPool.defaultNumberOfObjects"/> number of times.
        /// </summary>
        protected void PreloadObjectPoolObjects()
        {
            // Spawns the object and set them to inactive
            for (int i = 0; i < defaultNumberOfObjects; ++i)
            {
                CreateNewObject();
            }
        }

        /// <summary>
        /// Creates a new object and return it.
        /// Override if neccessary.
        /// </summary>
        /// <returns>Created GameObject</returns>
        protected virtual GameObject CreateNewObject()
        {
            GameObject newObject = Instantiate(originalObject, transform);
            newObject.SetActive(false);
            return newObject;
        }

        // Requires to be overriden since object pools usually have different rules to check whether that gameobject is "inactive"
        public abstract GameObject GetAvailableObject();
    }
}