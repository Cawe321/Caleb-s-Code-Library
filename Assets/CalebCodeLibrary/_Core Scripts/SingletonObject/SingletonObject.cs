using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalebCodeLibrary.SingletonObject
{
    /// <summary>
    /// A singleton template inherited from <see cref="MonoBehaviour"/> to make game objects follow the singleton design pattern. <see cref"Object.DontDestroyOnLoad(UnityEngine.Object)"></see> will also be applied to the object.
    /// </summary>
    public class SingletonObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get { return _instance; } }
        private static T _instance;

        public virtual void Awake()
        {
            SingletonAwake();
        }

        public void SingletonAwake()
        {
            Debug.Log("Awake");
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this as T;
                DontDestroyOnLoad(this); /// Ensures that object persists(still exists) when changing scenes.
            }
        }
    }
}
