using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalebCodeLibrary.SingletonObject.Example
{
    /// <summary>
    /// An example script that will call a function from <see cref="SingletonObject_ExampleScript"'s singleton./>
    /// </summary>
    public class SingletonObject_ExampleCallerScript : MonoBehaviour
    {
        void Start()
        {
            // We are calling a function from SingletonObject_ExampleScript's Singleton and retrieving the value that is returned by that function.
            int receivedValue = SingletonObject_ExampleScript.Instance.DummyFunctionThatReturns5();

            // Lets print the value to console.
            Debug.Log("Received value from Singleton: " + receivedValue.ToString());
        }
    }

}
